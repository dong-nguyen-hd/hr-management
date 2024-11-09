using API.Domain.Models;
using API.Domain.Services;
using API.Resources.DTOs.Information;
using API.Resources.DTOs.Timesheet;
using API.Results;
using Microsoft.Extensions.Options;
using OfficeOpenXml;

namespace API.Services;

public class TimesheetService : ResponseMessageService, ITimesheetService
{
    #region Property
    protected readonly IMapper Mapper;
    protected readonly IUnitOfWork UnitOfWork;
    private readonly HostResource _hostResource;
    private readonly IWebHostEnvironment _env;
    private readonly IPersonRepository _personRepository;
    private readonly ITimesheetRepository _timesheetRepository;

    private Timesheet _element = new();
    private List<Timesheet> _listElement = new();
    #endregion

    #region Constructor
    public TimesheetService(IPersonRepository personRepository,
        ITimesheetRepository timesheetRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IWebHostEnvironment env,
        IOptionsMonitor<HostResource> hostResource,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(responseMessage)
    {
        this.Mapper = mapper;
        this.UnitOfWork = unitOfWork;
        this._hostResource = hostResource.CurrentValue;
        this._env = env;
        this._personRepository = personRepository;
        this._timesheetRepository = timesheetRepository;
    }
    #endregion

    #region Method
    public async Task<BaseResult<TimesheetResource>> ImportAsync(Stream stream)
    {
        using (ExcelPackage package = new ExcelPackage())
        {
            await package.LoadAsync(stream);

            // Get the first worksheet in the workbook
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            int? year = worksheet.Cells[3, 2].GetValue<int?>();
            int? month = worksheet.Cells[3, 4].GetValue<int?>();

            if (year == null || month == null)
                return new BaseResult<TimesheetResource>(ResponseMessage.Values["Timesheet_Invalid"]);

            // Calculate timesheet
            var people = await _personRepository.GetAllAsync();
            string[] timesheet = new string[31];
            var totalRow = worksheet.Dimension.Rows;
            for (int row = 7; row < totalRow; row++)
            {
                string staffId = worksheet.Cells[row, 2].GetValue<string>();
                if (string.IsNullOrEmpty(staffId)) continue;

                var person = GetPersonByStaffId(people, staffId);

                if (person is null) continue;

                int day = 0;
                for (int tempCol = 6; tempCol < 37; tempCol++)
                {
                    timesheet[day] = worksheet.Cells[row, tempCol].GetValue<string>()?.Trim().ToUpper();
                    ConvertValueCell(timesheet[day]);

                    day++;
                }

                _element.TimesheetJSON = JsonConvert.SerializeObject(timesheet);
                _element.Date = new DateTime((int)year, (int)month, 1);
                _element.Person = person;
                _element.PersonId = person.Id;
                _listElement.Add(_element);
                _element = new();
            }

            // Add timesheets to db
            _timesheetRepository.AttachRange(_listElement);
            await UnitOfWork.CompleteAsync();

            // Save as new file
            string newPath = GetRootPath($"{year}-{month}.xlsx");
            if (File.Exists(newPath))
                File.Delete(newPath);

            await package.SaveAsAsync(newPath);
        }

        return new BaseResult<TimesheetResource>(true);
    }

    public async Task<BaseResult<TimesheetResource>> GetTimesheetByPersonIdAsync(int personId, DateTime date)
    {
        var timesheet = await _timesheetRepository.GetTimesheetByPersonIdAsync(personId, date);

        if (timesheet is null)
            return new BaseResult<TimesheetResource>(ResponseMessage.Values["NoData"]);

        // Mapping
        var resource = Mapper.Map<Timesheet, TimesheetResource>(timesheet);

        return new BaseResult<TimesheetResource>(resource);
    }

    #region Private work
    private string GetRootPath(string timesheetFileName)
    {
        string timesheetPath = string.Format($"{_hostResource.TimesheetPath}{timesheetFileName}");
        string rootPath = string.Concat(_env.WebRootPath, timesheetPath);

        return rootPath;
    }

    private Person GetPersonByStaffId(IEnumerable<Person> people, string staffId)
    {
        string tempStaffId = staffId.Trim();

        foreach (var person in people)
            if (person.StaffId.Equals(tempStaffId)) return person;

        return null;
    }

    private void ConvertValueCell(string valueCell)
    {
        if (string.IsNullOrEmpty(valueCell)) return;

        var cleanValue = valueCell.Trim().ToUpper();

        if (cleanValue.Equals("-") || cleanValue.Equals("O"))
            return;

        if (cleanValue.Equals("W"))
            _element.WorkDay += 1f;

        if (cleanValue.Equals("R"))
            _element.WorkDay += 0.5f;

        if (cleanValue.Equals("A"))
            _element.Absent += 1f;

        if (cleanValue.Equals("H"))
            _element.Holiday += 1f;

        if (cleanValue.Equals("S"))
            _element.UnpaidLeave += 1f;

        if (cleanValue.Equals("V"))
            _element.PaidLeave += 1f;

        _element.TotalWorkDay += 1;
    }
    #endregion

    #endregion
}