using Business.Resources.Technology;

namespace Business.Extensions;

static public class RelateElementOfList
{
    public static List<T> RemoveDuplicate<T>(this List<T> source) => new HashSet<T>(source).ToList();

    public static List<TechnologyResource> IntersectTechnology(this IEnumerable<TechnologyResource> total, string technologies)
    {
            string[] arrTechnologies = technologies.Split(',');
            List<TechnologyResource> listTechnologyResource = new List<TechnologyResource>(arrTechnologies.Length);

            foreach (var technology in total)
                foreach (var id in arrTechnologies)
                    if (technology.Id.Equals(Convert.ToInt32(id)))
                        listTechnologyResource.Add(technology);

            return listTechnologyResource;
        }
}