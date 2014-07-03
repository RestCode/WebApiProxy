using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using DocsByReflection;
using WebApiProxy.Core.Models;

namespace WebApiProxy.Server
{
    public class MetadataProvider
    {
        List<ModelDefinition> models = new List<ModelDefinition>();

        public Metadata GetMetadata(HttpRequestMessage request)
        {
            var host = request.RequestUri.Scheme + "://" + request.RequestUri.Authority;
            var descriptions = GlobalConfiguration.Configuration.Services.GetApiExplorer().ApiDescriptions;
            var documentationProvider = GlobalConfiguration.Configuration.Services.GetDocumentationProvider();

            ILookup<HttpControllerDescriptor, ApiDescription> apiGroups = descriptions
                .Where(a => !a.RelativePath.Contains("Swagger") && !a.RelativePath.Contains("docs"))
                .ToLookup(a => a.ActionDescriptor.ControllerDescriptor);

            var metadata = new Metadata
            {
                Definitions = from d in apiGroups
                              where !d.Key.ControllerType.IsExcluded()
                              select new ControllerDefinition
                              {
                                  Name = d.Key.ControllerName,
                                  Description = documentationProvider == null ? "" : documentationProvider.GetDocumentation(d.Key) ?? "",
                                  ActionMethods = from a in descriptions
                                                  where !a.ActionDescriptor.ControllerDescriptor.ControllerType.IsExcluded()
                                                  && !a.RelativePath.Contains("Swagger")
                                                  && !a.RelativePath.Contains("docs")
                                                  && a.ActionDescriptor.ControllerDescriptor.ControllerName == d.Key.ControllerName
                                                  select new ActionMethodDefinition
                                                  {
                                                      Name = a.ActionDescriptor.ActionName,
                                                      BodyParameter = (from b in a.ParameterDescriptions
                                                                       where b.Source == ApiParameterSource.FromBody
                                                                       select new ParameterDefinition
                                                                       {
                                                                           Name = b.ParameterDescriptor.ParameterName,
                                                                           Type = ParseType(b.ParameterDescriptor.ParameterType),
                                                                           Description = b.Documentation ?? ""
                                                                       }).FirstOrDefault(),
                                                      UrlParameters = from b in a.ParameterDescriptions
                                                                      where b.Source == ApiParameterSource.FromUri
                                                                      select new ParameterDefinition
                                                                      {
                                                                          Name = b.ParameterDescriptor.ParameterName,
                                                                          Type = ParseType(b.ParameterDescriptor.ParameterType),
                                                                          Description = b.Documentation ?? ""
                                                                      },
                                                      Url = a.RelativePath,

                                                      Description = a.Documentation ?? "",
                                                      ReturnType = ParseType(a.ActionDescriptor.ReturnType),
                                                      Type = a.HttpMethod.Method
                                                  }
                              },
                Models = models,
                Host = host
            };

            return metadata;

        }

        private string ParseType(Type type)
        {
            string res;

            if (type == null)
                return "";

            //If the type is a generic type format to correct class name
            if (type.IsGenericType)
            {
                res = type.Name;

                int index = res.IndexOf('`');

                if (index > -1)
                    res = res.Substring(0, index);

                Type[] args = type.GetGenericArguments();

                res += "<";

                for (int i = 0; i < args.Length; i++)
                {
                    if (i > 0)
                        res += ", ";
                    //Recursivly find nested arguments
                    res += ParseType(args[i]);
                }
                res += ">";
            }
            else
            {
                if (type.ToString().StartsWith("System."))
                {
                    if (type.ToString().Equals("System.Void"))
                        res = "void";
                    else
                        res = type.Name;
                }
                else
                {
                    res = type.Name;

                    if (!models.Any(c => c.Name.Equals(type.Name)) && !type.IsInterface)
                        AddModelDefinition(type);
                }
            }

            return res;
        }

        private void AddModelDefinition(Type classToDef)
        {
            //When the class is an array redefine the classToDef as the array type
            if (classToDef.IsArray)
            {
                classToDef = classToDef.GetElementType();
            }
            //If the class has not been mapped then map into metadata
            if (!models.Any(c => c.Name.Equals(classToDef.Name)))
            {

                ModelDefinition model = new ModelDefinition();

                model.Name = classToDef.Name;

                var constants = classToDef
                    .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                    .Where(f => f.IsLiteral && !f.IsInitOnly)
                    .ToList();

                model.Constants = from constant in constants
                                  select new ConstantDefinition
                                  {
                                      Name = constant.Name,
                                      Type = ParseType(constant.FieldType),
                                      Value = constant.GetRawConstantValue().ToString(),
                                      Description = GetDescription(constant)
                                  };

                var properties = classToDef.GetProperties();
                model.Properties = from property in properties
                                   select new ModelProperty
                                   {
                                       Name = property.Name,
                                       Type = ParseType(property.PropertyType)
                                   };

                models.Add(model);
            }
        }

        private static string GetDescription(FieldInfo constant)
        {
            var xml = DocsService.GetXmlFromMember(constant, false);

            if (xml != null)
            {
                return xml.InnerText;
            }

            return String.Empty;
        }
    }
}
