using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestNet.Core.Options
{
    public class AppSettings
    {
        public List<MockapiIO> MockapiIO;
    }

    public class MockapiIO
    {
        public MockapiIO(string name, string url)
        {
            Name = name;
            Url = url;
        }  

        public string Name { get; set; }
        public string Url { get; set; }
    }
}
