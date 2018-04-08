using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiSample.BL.Interfaces;
using ApiSample.Models;
using ApiSample.DA.Interfaces;

namespace ApiSample.BL.Services
{
    public class SampleService : ISampleService
    {
        public ISampleRepository SampleRepository { get; set; }

        //public SampleService() : this(new SampleRepository())
        //{

        //}

        public SampleService(ISampleRepository sampleRepository)
        {
            SampleRepository = sampleRepository;
        }

        public IEnumerable<SampleModel> GetSamples()
        {
            return SampleRepository.GetSamples();
        }
    }
}
