using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Papara.Core.Entites;
using Papara.Core.Enums;
using Papara.Core.Interfaces;
using Papara.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Papara.Api.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberRepository _repository;
        private readonly Func<CacheTech, ICacheService> _cacheService;
        private IMapper _mapper;

        public MemberController(IMemberRepository repository, Func<CacheTech, ICacheService> cacheService, IMapper mapper)
        {
            _repository = repository;
            _cacheService = cacheService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task GetDataFromApi()

        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://jsonplaceholder.typicode.com/posts ");
            webRequest.Method = "GET";

            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            Console.WriteLine(webResponse.StatusCode);
            Console.WriteLine(webResponse.Server);

            string Json;
            using (Stream stream = webResponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                Json = reader.ReadToEnd();
            }
            List<MemberDTO> items = (List<MemberDTO>)JsonConvert.DeserializeObject(Json, typeof(List<MemberDTO>));
            var map = _mapper.Map<List<Member>>(items);

            foreach (var item in map)
            {
                await _repository.AddAsync(item);
            }

            Console.WriteLine(items);


        }
        [HttpGet]
        [Route("GetDataFromApi")]
        public IActionResult RetrieveData()
        {
            RecurringJob.AddOrUpdate(() => GetDataFromApi(), "*/5 * * * * *");
            return Ok($"Data will be retrieved every 5 minutes from API");
        }

        [HttpGet]
        public IActionResult GetAllAsync()
        {
            var members = _repository.GetAllAsync();
            return Ok(members);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var member = await _repository.GetByIdAsync(id);
            return Ok(member);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Member member)
        {
            await _repository.DeleteAsync(member);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Member member)
        {
            await _repository.UpdateAsync(member);
            return Ok();
        }
    }
}
