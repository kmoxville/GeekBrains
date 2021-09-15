using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;

namespace CmdParser
{
    public class Parser
    {
        public Parser(string[] args)
        {
            if ((args?.Length ?? 0) == 0)
                throw new ArgumentNullException("Args is null or zero length");

            _args = args;   
        }

        public bool HadErrors => _errors.Count != 0;
        public string Usage => GetUsage();

        public void Parse(object options)
        {
            PropertyInfo[] properties = options.GetType().GetProperties();
            FieldInfo[] fields = options.GetType().GetFields();

            _optionsDescription = GetOptionsDescription(options, properties);
            _options.AddRange(GetOptionPropertiesAndFields(options, fields));
            _options.AddRange(GetOptionPropertiesAndFields(options, properties));

            if (_options.Count == 0)
                throw new ArgumentException("Options argument has no members with Option attribute");

            for (int i = 0; i < _args.Length; i++)
            {
                string arg = _args[i];

                try
                {
                    var optionAndMemberInfo = _options.Where(option => option.Item1.ShortName == arg || option.Item1.FullName == arg).First(); //InvalidOperationException
                    Option option           = optionAndMemberInfo.Item1;
                    MemberInfo memberInfo   = optionAndMemberInfo.Item2;

                    if (option.HasValue)
                    {
                        if (i == _args.Length - 1)
                            throw new ParsingException($"Option {option.FullName} requires value") { Argument = arg };

                        Type type = ((memberInfo as PropertyInfo)?.PropertyType ?? (memberInfo as FieldInfo)?.FieldType);
                        object parsedValue = (type == typeof(string) ? _args[++i] : TryParse(type, _args[++i]));

                        SetOptionValue(options, memberInfo, parsedValue);
                    }
                    else
                    {
                        SetOptionValue(options, memberInfo, true);
                    }
                }
                catch (InvalidOperationException) 
                {
                    _errors.Add(new ParsingException("option not found") { Argument = arg });
                }
                catch (ParsingException pe)
                {
                    _errors.Add(pe);
                }
            }
        }

        public string GetUsage()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(_optionsDescription);
            sb.AppendLine("Available options:");

            foreach (Option option in _options.Select(element => element.Item1))
            {
                sb.AppendLine($"{option.ShortName}, {option.FullName}: {option.HelpText}");
            }

            return sb.ToString();
        }

        private void SetOptionValue(object options, MemberInfo memberInfo, object value)
        {
            if (memberInfo is PropertyInfo propertyInfo)
            {
                propertyInfo.SetValue(options, value);
            }
            else if (memberInfo is FieldInfo fieldInfo)
            {
                fieldInfo.SetValue(options, value);
            }
        }

        private object TryParse(Type type, string value)
        {
            object result;

            MethodInfo parseMethodInfo = type.GetMethod("Parse", new[] { typeof(string) });
            result = parseMethodInfo?.Invoke(null, new object[] { value });

            return result;
        }

        private List<(Option, MemberInfo)> GetOptionPropertiesAndFields(object options, MemberInfo[] fis)
        {
            Type type = options.GetType();
            var fieldsWithOptionAttributeQuery = fis
                .Where(field => field.GetCustomAttribute(typeof(Option)) != null)
                .Select(field => ((Option)field.GetCustomAttribute(typeof(Option)), field));

            return fieldsWithOptionAttributeQuery.ToList();
        }

        private string GetOptionsDescription(object options, MemberInfo[] fis)
        {
            string result = string.Empty;

            try
            {
                Type type = options.GetType();
                PropertyInfo descriptionMemberInfo = (PropertyInfo)fis
                    .Where(field => field.GetCustomAttribute(typeof(OptionsDescription)) != null)
                    .FirstOrDefault();

                result = (string)descriptionMemberInfo.GetValue(options);
            }
            catch(Exception) { }

            return result;
        }

        private string[]                    _args;
        private string                      _optionsDescription = string.Empty;
        private List<(Option, MemberInfo)>  _options = new List<(Option, MemberInfo)>();
        private List<ParsingException>      _errors = new List<ParsingException>();
    }
}
