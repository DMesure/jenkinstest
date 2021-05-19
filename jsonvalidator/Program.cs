using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace jsonvalidator
{
    class Program
    {
        static void Main(string[] args)
        {
            string schemaJson = @"{
              'description': 'A person',
              'type': 'object',
              'properties': {
                'name': {'type': 'string'},
                'hobbies': {
                  'type': 'array',
                  'items': {'type': 'string'}
                }
              }
            }";

            JSchema schema = JSchema.Parse(schemaJson);

            JObject person = JObject.Parse(@"{
                'name': null,
                'hobbies': ['Invalid content', 0.123456789]
            }");

            IList<string> messages;
            bool valid = person.IsValid(schema, out messages);
            Console.WriteLine(valid);
            foreach (string str in messages)
            {
                Console.WriteLine(str);
            }

            Console.WriteLine("-------------------------------------------------------------------------------------");

            JObject o1 = JObject.Parse(File.ReadAllText(@"..\..\..\config.json"));
            //Console.WriteLine(o1.ToString());

            //using StreamReader file = File.OpenText(@"C:\Users\mesureda\Desktop\DMesure\.NET\jsonvalidator\jsonvalidator\config.json");
            //using JsonTextReader reader = new JsonTextReader(file);
            //JObject o2 = (JObject)JToken.ReadFrom(reader);

            JSchema s1 = JSchema.Parse(File.ReadAllText(@"..\..\..\configScheme.json"));
            //Console.WriteLine(s1.ToString());


            IList<string> messages2;
            bool valid2 = o1.IsValid(s1, out messages2);
            Console.WriteLine(valid2);
            foreach (string str in messages2)
            {
                Console.WriteLine(str);
            }
        }
    }
}
