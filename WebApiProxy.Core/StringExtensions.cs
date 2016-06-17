using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebApiProxy
{
    public static class StringExtensions
    {
        public static string DefaultIfEmpty(this string helper, string value)
        {
            if (String.IsNullOrEmpty(helper))
                return value;

            return helper;
        }

        public static string ToTitle(this string helper)
        {
            return helper.Substring(0, 1).ToUpper() + helper.Substring(1, helper.Length - 1).ToLower();

        }

        public static string ToCamelCasing(this string helper)
        {
            return helper.Replace(helper[0].ToString(), helper[0].ToString().ToLower());
        }

        /// <summary>
        /// Transform the string to the XML documentation expected format respecting the new lines.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns>The xml documentation format.</returns>
        public static string ToSummary(this string description)
        {
            return Regex.Replace(description, "\n\\s*", "\n\t\t/// ");
        }

        /// <summary>
        /// Ensures common return types are made conrete
        /// </summary>
        public static string ToConcrete(this string returnType)
        {
            if (string.IsNullOrWhiteSpace(returnType))
                return returnType;

            string[] commonTypes = { 
                "IQueryable", 
                "IList", 
                "IEnumerable"
            };

            foreach (var type in commonTypes)
            {
                var replaced = Regex.Replace(returnType, type + @"\<(.+)\>", "List<$1>");

                if (replaced.Length != returnType.Length)
                    return replaced;
            }

            return returnType;
        }

        public static string Format(this string pattern, object template)
        {
            var rePattern = new Regex(@"(\{+)([^\}]+)(\}+)", RegexOptions.Compiled);

            if (template == null) throw new ArgumentNullException();
            Type type = template.GetType();
            var cache = new Dictionary<string, string>();
            return rePattern.Replace(pattern, match =>
            {
                int lCount = match.Groups[1].Value.Length,
                    rCount = match.Groups[3].Value.Length;
                if ((lCount % 2) != (rCount % 2)) throw new InvalidOperationException("Unbalanced braces");
                string lBrace = lCount == 1 ? "" : new string('{', lCount / 2),
                    rBrace = rCount == 1 ? "" : new string('}', rCount / 2);

                string key = match.Groups[2].Value, value;
                if (lCount % 2 == 0)
                {
                    value = key;
                }
                else
                {
                    if (!cache.TryGetValue(key, out value))
                    {
                        var prop = type.GetProperty(key);
                        if (prop == null)
                        {
                            throw new ArgumentException("Not found: " + key, "pattern");
                        }
                        value = Convert.ToString(prop.GetValue(template, null));
                        cache.Add(key, value);
                    }
                }
                return lBrace + value + rBrace;
            });
        }
    }
}
