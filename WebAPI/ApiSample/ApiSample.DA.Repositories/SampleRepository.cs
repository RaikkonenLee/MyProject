using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiSample.DA.Interfaces;
using ApiSample.Models;

namespace ApiSample.DA.Repositories
{
    public class SampleRepository : ISampleRepository
    {
        public IEnumerable<SampleModel> GetSamples()
        {
            for (int i0 = 0; i0 < 10; i0++)
            {
                yield return new SampleModel()
                {
                    Id = i0,
                    Data = string.Format("Data - {0}", i0),
                    CreateAt = DateTime.Now
                };
            }
        }
    }
}
