using Autodesk.Revit.DB;

namespace Case.ViewCreator
{
    public class clsParameterData
    {

        /// <summary>
        /// Get a parameter from a view by name
        /// </summary>
        /// <param name="plan"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static Parameter GetParameterByName(ViewPlan plan, string paramName)
        {
            foreach (Parameter parameter in plan.Parameters)
            {
                if (parameter.Definition.Name == paramName)
                {
                    return parameter;
                }
            }
            return null;
        }

    }
}
