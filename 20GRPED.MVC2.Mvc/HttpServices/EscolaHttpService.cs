﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace _20GRPED.MVC2.Mvc.HttpServices
{
    public class EscolaHttpService : IEscolaHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptionsMonitor<BibliotecaHttpOptions> _bibliotecaHttpOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<IdentityUser> _signInManager;

        public EscolaHttpService(
            IHttpClientFactory httpClientFactory,
            IOptionsMonitor<BibliotecaHttpOptions> bibliotecaHttpOptions,
            IHttpContextAccessor httpContextAccessor,
            SignInManager<IdentityUser> signInManager)
        {
            _bibliotecaHttpOptions = bibliotecaHttpOptions ?? throw new ArgumentNullException(nameof(bibliotecaHttpOptions));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _signInManager = signInManager;
            ;

            _httpClient = httpClientFactory?.CreateClient(bibliotecaHttpOptions.CurrentValue.Name) ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpClient.Timeout = TimeSpan.FromMinutes(_bibliotecaHttpOptions.CurrentValue.Timeout);
        }

        private async Task<bool> AddAuthJwtToRequest()
        {
            var jwtCookieExists = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("bibliotecaToken", out var jwtFromCookie);
            if (!jwtCookieExists)
            {
                await _signInManager.SignOutAsync();
                return false;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtFromCookie);
            return true;
        }

        public async Task<IEnumerable<EscolaEntity>> GetAllAsync()
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return null;
            }
            var httpResponseMessage = await _httpClient.GetAsync(_bibliotecaHttpOptions.CurrentValue.EscolaPath);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
                return null;
            }

            return JsonConvert.DeserializeObject<List<EscolaEntity>>(await httpResponseMessage.Content.ReadAsStringAsync());
        }

        public async Task<EscolaEntity> GetByIdAsync(int id)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return null;
            }
            var pathWithId = $"{_bibliotecaHttpOptions.CurrentValue.EscolaPath}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                //await _signInManager.SignOutAsync();
                return null;
            }

            return JsonConvert.DeserializeObject<EscolaEntity>(await httpResponseMessage.Content.ReadAsStringAsync());
        }

        public async Task<HttpResponseMessage> GetByIdHttpAsync(int id)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return null;
            }
            var pathWithId = $"{_bibliotecaHttpOptions.CurrentValue.EscolaPath}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);

            return httpResponseMessage;
        }

        public async Task InsertAsync(EscolaEntity insertedEntity)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
            var uriPath = $"{_bibliotecaHttpOptions.CurrentValue.EscolaPath}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(insertedEntity), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(uriPath, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task UpdateAsync(EscolaEntity updatedEntity)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
            var pathWithId = $"{_bibliotecaHttpOptions.CurrentValue.EscolaPath}/{updatedEntity.Id}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(updatedEntity), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PutAsync(pathWithId, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var jwtSuccess = await AddAuthJwtToRequest();
            if (!jwtSuccess)
            {
                return;
            }
            await AddAuthJwtToRequest();
            var pathWithId = $"{_bibliotecaHttpOptions.CurrentValue.EscolaPath}/{id}";
            var httpResponseMessage = await _httpClient.DeleteAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }
    }
}
