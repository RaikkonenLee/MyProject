using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiSample.BL.Interfaces;
using ApiSample.Models;
using ApiSample.DA.Interfaces;

namespace ApiSample.BL.TempServices
{
    public class TempSampleService : ISampleService
    {
        public ISampleRepository SampleRepository { get; set; }

        public TempSampleService(ISampleRepository sampleRepository)
        {
            SampleRepository = sampleRepository;
        }

        public IEnumerable<SampleModel> GetSamples()
        {
            return SampleRepository.GetSamples().Take(3);
        }
    }
}
