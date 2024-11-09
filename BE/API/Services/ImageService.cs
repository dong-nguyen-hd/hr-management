using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using API.Domain.Services;
using API.Resources.DTOs.Account;
using API.Resources.DTOs.Information;
using API.Resources.DTOs.Person;
using API.Resources.Enums;
using API.Results;
using Microsoft.Extensions.Options;
using Image = System.Drawing.Image;
using Size = System.Drawing.Size;

namespace API.Services;

public class ImageService : ResponseMessageService, IImageService
{
    #region Property
    private readonly IPersonRepository _personRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly HostResource _hostResource;
    private readonly IWebHostEnvironment _env;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public ImageService(IPersonRepository personRepository,
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork,
        IWebHostEnvironment env,
        IMapper mapper,
        IOptionsMonitor<HostResource> hostResource,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(responseMessage)
    {
        this._personRepository = personRepository;
        this._accountRepository = accountRepository;
        this._unitOfWork = unitOfWork;
        this._hostResource = hostResource.CurrentValue;
        this._env = env;
        this._mapper = mapper;
    }
    #endregion

    #region Method
    public async Task<BaseResult<AccountResource>> ResetAccountAvatarAsync(int id)
    {
        try
        {
            // Validate Id is existent?
            var tempAccount = await _accountRepository.GetByIdAsync(id);
            if (tempAccount is null)
                return new BaseResult<AccountResource>(ResponseMessage.Values["Account_NoData"]);

            DeleteFileByName(tempAccount.Avatar);
            tempAccount.Avatar = Constant.DefaultAvatar; // Reset avatar to default

            await _unitOfWork.CompleteAsync();

            return new BaseResult<AccountResource>(_mapper.Map<AccountResource>(tempAccount));
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Account_Saving_Error"], ex);
        }
    }

    public async Task<BaseResult<PersonResource>> ResetPersonAvatarAsync(int id)
    {
        try
        {
            // Validate Id is existent?
            var tempPerson = await _personRepository.GetByIdAsync(id);
            if (tempPerson is null)
                return new BaseResult<PersonResource>(ResponseMessage.Values["Person_Id_NoData"]);

            DeleteFileByName(tempPerson.Avatar);
            tempPerson.Avatar = Constant.DefaultAvatar; // Reset avatar to default

            await _unitOfWork.CompleteAsync();

            return new BaseResult<PersonResource>(_mapper.Map<PersonResource>(tempPerson));
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Person_Saving_Error"], ex);
        }
    }

    public async Task<BaseResult<AccountResource>> SaveImageAccountAsync(int accountId, Stream imageStream)
    {
        try
        {
            using (var rawImage = Image.FromStream(imageStream))
            {
                var fileExtension = GetFilenameExtension(rawImage.RawFormat);

                // Validate properties of image
                var validateImage = ValidateImage<AccountResource>(imageStream, fileExtension);
                if (!validateImage.isSuccess)
                    return validateImage.result;

                // Validate Id is existent?
                var tempAccount = await _accountRepository.GetByIdAsync(accountId);
                if (tempAccount is null)
                    return new BaseResult<AccountResource>(ResponseMessage.Values["Account_Id_NoData"]);

                string avatarFileName = CreateFileName(tempAccount.UserName);
                tempAccount.Avatar = Initialize(tempAccount.Avatar, avatarFileName, rawImage);

                await _unitOfWork.CompleteAsync();

                return new BaseResult<AccountResource>(_mapper.Map<AccountResource>(tempAccount));
            }
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Image_Saving_Error"], ex);
        }
    }

    public async Task<BaseResult<PersonResource>> SaveImagePersonAsync(int personId, Stream imageStream)
    {
        try
        {
            using (var rawImage = Image.FromStream(imageStream))
            {
                var fileExtension = GetFilenameExtension(rawImage.RawFormat);

                // Validate properties of image
                var validateImage = ValidateImage<PersonResource>(imageStream, fileExtension);
                if (!validateImage.isSuccess)
                    return validateImage.result;

                // Validate Id is existent?
                var tempPerson = await _personRepository.GetByIdAsync(personId);
                if (tempPerson is null)
                    return new BaseResult<PersonResource>(ResponseMessage.Values["Person_Id_NoData"]);

                string avatarFileName = CreateFileName(tempPerson.StaffId);
                tempPerson.Avatar = Initialize(tempPerson.Avatar, avatarFileName, rawImage);

                await _unitOfWork.CompleteAsync();

                return new BaseResult<PersonResource>(_mapper.Map<PersonResource>(tempPerson));
            }
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Image_Saving_Error"], ex);
        }
    }

    #region Private work
    private bool DeleteFileByName(string avatarName)
    {
        var path = GetRootPath(avatarName);

        bool one = DeletePhotoIfExist(path.originalPath);
        bool two = DeletePhotoIfExist(path.thumbnailPath);

        return one && two ? true : false;
    }

    private static string CreateFileName(string avatarName)
        => $"{avatarName}-{Guid.NewGuid()}.jpg";

    private static string GetFilenameExtension(ImageFormat imageFormat)
    {
        return ImageCodecInfo.GetImageEncoders()
            .First(x => x.FormatID == imageFormat.Guid)
            .FilenameExtension
            .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
            .FirstOrDefault()
            .Trim(new[] { '*', '.' });
    }

    private static bool CheckExistentAvatar(string avatarName)
        => (string.IsNullOrEmpty(avatarName) || avatarName.Equals(Constant.DefaultAvatar)) ? false : true;

    private string Initialize(string oldFileName, string newFileName, Image rawImage)
    {
        string defaultName = Constant.DefaultAvatar;

        try
        {
            bool isExistent = CheckExistentAvatar(oldFileName);
            var newPath = GetRootPath(newFileName);

            var oldPath = GetRootPath(oldFileName);
            var tempPath = GetTemporaryPath(oldFileName);

            // Change file name if it's existent
            if (isExistent)
            {
                bool isSuccesschangefilename = false;
                isSuccesschangefilename = ChangeFileName(oldPath.originalPath, tempPath.originalPath);
                isSuccesschangefilename = ChangeFileName(oldPath.thumbnailPath, tempPath.thumbnailPath);

                if (!isSuccesschangefilename)
                    return defaultName;
            }

            bool isSuccess = SetThumbnails(rawImage, newPath.originalPath, newPath.thumbnailPath);

            if (isSuccess && isExistent)
            {
                DeletePhotoIfExist(tempPath.originalPath);
                DeletePhotoIfExist(tempPath.thumbnailPath);
            }
            else if (isExistent)
            {
                ChangeFileName(tempPath.originalPath, oldPath.originalPath);
                ChangeFileName(tempPath.thumbnailPath, oldPath.thumbnailPath);

                return oldFileName;
            }

            return newFileName;
        }
        catch
        {
            return defaultName;
        }
    }

    private (string thumbnailPath, string originalPath) GetRootPath(string avatarFileName)
    {
        string originalPath = string.Format($"{_hostResource.OriginalImagePath}{avatarFileName}");
        string rootOriginalPath = string.Concat(_env.WebRootPath, originalPath);

        string thumbnailPath = string.Format($"{_hostResource.ThumbnailImagePath}{avatarFileName}");
        string rootThumbnailPath = string.Concat(_env.WebRootPath, thumbnailPath);

        return (rootThumbnailPath, rootOriginalPath);
    }

    private (string thumbnailPath, string originalPath) GetTemporaryPath(string avatarFileName)
    {
        var guid = Guid.NewGuid().ToString();

        string originalPath = string.Format($"{_hostResource.OriginalImagePath}{guid}{avatarFileName}");
        string rootOriginalPath = string.Concat(_env.WebRootPath, originalPath);

        string thumbnailPath = string.Format($"{_hostResource.ThumbnailImagePath}{guid}{avatarFileName}");
        string rootThumbnailPath = string.Concat(_env.WebRootPath, thumbnailPath);

        return (rootThumbnailPath, rootOriginalPath);
    }

    private bool DeletePhotoIfExist(string path)
    {
        if (!File.Exists(path))
            return false;

        File.Delete(path);

        return true;
    }

    private bool ChangeFileName(string sourceFileName, string destFileName)
    {
        if (!File.Exists(sourceFileName) || File.Exists(destFileName))
            return false;

        File.Move(sourceFileName, destFileName);

        return true;
    }

    private bool SetThumbnails(Image rawImage, string originalPath, string thumbnailPath)
    {
        try
        {
            // Set Correct Orientation
            SetCorrectOrientation(rawImage);

            CreateAndSaveImage(rawImage, 500, originalPath);
            CreateAndSaveImage(rawImage, 100, thumbnailPath);

            return true;
        }
        catch
        {
            return false;
        }
    }

    private static void SetCorrectOrientation(Image image)
    {
        // Property Id = 274 describe EXIF orientation parameter
        if (Array.IndexOf(image.PropertyIdList, 274) > -1)
        {
            var orientation = (int)image.GetPropertyItem(274).Value[0];
            switch (orientation)
            {
                case 1:
                    // No rotation required.
                    break;
                case 2:
                    image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;
                case 3:
                    image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case 4:
                    image.RotateFlip(RotateFlipType.Rotate180FlipX);
                    break;
                case 5:
                    image.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                case 6:
                    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case 7:
                    image.RotateFlip(RotateFlipType.Rotate270FlipX);
                    break;
                case 8:
                    image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }
            // This EXIF data is now invalid and should be removed.
            image.RemovePropertyItem(274);
        }
    }

    private void CreateAndSaveImage(Image image, int size, string imagePath)
    {
        var thumbnailSize = GetThumbnailSize(image, size);

        using (var bitmap = ResizeImage(image, thumbnailSize.Width, thumbnailSize.Height))
        {
            bitmap.Save(imagePath, ImageFormat.Jpeg);
        }
    }

    private static Size GetThumbnailSize(Image original, int size = 500)
    {
        var originalWidth = original.Width;
        var originalHeight = original.Height;

        double factor;
        if (originalWidth > originalHeight)
        {
            factor = (double)size / originalWidth;
        }
        else
        {
            factor = (double)size / originalHeight;
        }

        return new Size((int)(originalWidth * factor), (int)(originalHeight * factor));
    }

    private Bitmap ResizeImage(Image image, int width, int height)
    {
        var bitmapResult = new Bitmap(width, height);

        using (var graphics = Graphics.FromImage(bitmapResult))
        {
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            graphics.DrawImage(image, 0, 0, bitmapResult.Width, bitmapResult.Height);
        }

        return bitmapResult;
    }

    private (bool isSuccess, BaseResult<T> result) ValidateImage<T>(Stream imageStream, string imageFormat)
    {
        List<string> messages = new();

        // Validate properties of image
        if (imageStream is null || imageStream.Length == 0)
            messages.Add(ResponseMessage.Values["Image_NoData"]);
        if (imageStream?.Length > 5242880) // 5 MB
            messages.Add(ResponseMessage.Values["Image_Bigger_Error"]);
        if (!ValidateImageFormat(imageFormat))
            messages.Add(ResponseMessage.Values["Image_Format_Error"]);

        return messages.Count == 0 ? (true, null) : (false, new BaseResult<T>(messages));
    }

    private static bool ValidateImageFormat(string imageFormat)
    {
        if (imageFormat.Equals("JPG"))
            return true;

        if (imageFormat.Equals("PNG"))
            return true;

        if (imageFormat.Equals("GIF"))
            return true;

        if (imageFormat.Equals("BMP"))
            return true;

        return false;
    }
    #endregion

    #endregion
}