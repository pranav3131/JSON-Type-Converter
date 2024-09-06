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


            // Combine properties and methods onto a different anonymous object
            var result = new
            {
                ClassName = classname,
                Properties = properties,

            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        //New Action method for only genearating JSON Values of the properties when clicked on "Generate JSON" button
        public JsonResult generateJSON(string classname, string[] names, string[] types, string[] values)
        {

            if (!string.IsNullOrWhiteSpace(classname) && classname.Trim().Equals("Employee", StringComparison.OrdinalIgnoreCase))
            {
                var properties = names.Select((name, index) => new
                {
                    Name = name,
                    Type = types[index],
                    Value = values[index]
                }).ToList();

                // Create a JSON representation of the class properties
                var jsonresult = new
                {
                    ClassName = classname,
                    Properties = properties
                };
                return Json(jsonresult, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json("Class not found", JsonRequestBehavior.AllowGet);
            }
 
            }
         

        }
    }

