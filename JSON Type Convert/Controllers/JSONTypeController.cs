using JSON_Type_Convert.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Reflection;

namespace JSON_Type_Convert.Controllers
{
    public class JSONTypeController : Controller
    {
        // GET: JSONType
        public ActionResult Index()
        {
            return View();
        }


        /* An Action method which uses Reflection to fetch the Class/Model
           details and returns them to a view */
        public JsonResult getclassinfo(string classname)
        {
            Type classtype = null;

            // Check if the String classname = Employee (our class name) & if not return not found
            if (!string.IsNullOrWhiteSpace(classname) && classname.Trim().Equals("Employee", StringComparison.OrdinalIgnoreCase))
            {
                classtype = typeof(Employee); // We get the type of the employee class
            }
            else
            {
                return Json("Class not found", JsonRequestBehavior.AllowGet); // Return an error if class not found 
            }



            // Get the properties of the class
            var properties = classtype.GetProperties(BindingFlags.Public | BindingFlags.Instance) // BindingFlags.Public => Only incl. Public Props.
                                                                                                  // BindingFlags.Instance => Only incl. instance Props.          
            .Select(p => new { name = p.Name, Type = p.PropertyType.Name }) /* .select is a LINQ(Language integrated query)
                                                                             which transforms each property into an anonymous new obj */
            .ToList();


            // Get the methods of the class 
            var methods = classtype.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly) // BindingFlags.DeclaredOnly => Only incl.Methods declared in the class not inherited ones.

            .Select(m => new { name = m.Name, Type = m.ReturnType.Name }) // Methods use ReturnType whereas properties use PropertyType
            .ToList();



            // Combine properties and methods onto a different anonymous object
            var result = new
            {
                ClassName = classname,
                Properties = properties,
                Methods = methods
            };


            return Json(result, JsonRequestBehavior.AllowGet); // Return the result in Json format accessible to client side of the application.
        }
    }
}
