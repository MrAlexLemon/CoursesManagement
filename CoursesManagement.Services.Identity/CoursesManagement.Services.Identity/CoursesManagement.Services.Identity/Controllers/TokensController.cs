﻿using CoursesManagement.Common.Auth;
using CoursesManagement.Common.ErrorHandler;
using CoursesManagement.Services.Identity.Messages.Commands;
using CoursesManagement.Services.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManagement.Services.Identity.Controllers
{
    [Route("")]
    [ApiController]
    [JwtAuth]
    public class TokensController : BaseController
    {
        private readonly IAccessTokenService _accessTokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public TokensController(IAccessTokenService accessTokenService,
            IRefreshTokenService refreshTokenService)
        {
            _accessTokenService = accessTokenService;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost("access-tokens/{refreshToken}/refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshAccessToken(string refreshToken, RefreshAccessToken command)
            => Ok(await _refreshTokenService.CreateAccessTokenAsync(command.Bind(c => c.Token, refreshToken).Token));

        [HttpPost("access-tokens/revoke")]
        public async Task<IActionResult> RevokeAccessToken(RevokeAccessToken command)
        {
            await _accessTokenService.DeactivateCurrentAsync(
                command.Bind(c => c.UserId, UserId).UserId.ToString("N"));

            return NoContent();
        }

        [HttpPost("refresh-tokens/{refreshToken}/revoke")]
        public async Task<IActionResult> RevokeRefreshToken(string refreshToken, RevokeRefreshToken command)
        {
            await _refreshTokenService.RevokeAsync(command.Bind(c => c.Token, refreshToken).Token,
                command.Bind(c => c.UserId, UserId).UserId);

            return NoContent();
        }
    }
}
