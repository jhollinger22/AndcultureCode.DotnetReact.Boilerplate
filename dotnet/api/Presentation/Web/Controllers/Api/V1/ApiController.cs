using System.Collections.Generic;
using AndcultureCode.CSharp.Core.Enumerations;
using AndcultureCode.CSharp.Core.Models;
using Microsoft.Extensions.Localization;
using AndcultureCode.GB.Business.Core.Extensions;
using AndcultureCode.GB.Business.Core.Interfaces.Localization;

namespace AndcultureCode.GB.Presentation.Web.Controllers.Api.V1
{
    public abstract class ApiController : Controller
    {
        #region Constants

        protected const string HEADER_USER_AGENT = "User-Agent";

        #endregion Constants


        #region Constructors

        public ApiController(IStringLocalizer localizer) : base(localizer) { }

        #endregion Constructors


        #region Protected Properties

        public virtual ApiClaimsPrincipal ApiClaimsPrincipal { get; set; }

        public virtual ICulture ApiCulture { get; set; }

        /// <summary>
        /// Current authenticated user's selected role id
        /// </summary>
        public virtual long? CurrentRoleId => ApiClaimsPrincipal != null ? ApiClaimsPrincipal.RoleId : User.RoleId();

        /// <summary>
        /// Current authenticated user's role ids
        /// </summary>
        protected virtual string[] CurrentRoleIds => ApiClaimsPrincipal != null ? ApiClaimsPrincipal.RoleIds : User.RoleIds();


        /// <summary>
        /// Current authenticated user's id
        /// </summary>
        public virtual long? CurrentUserId => ApiClaimsPrincipal != null ? ApiClaimsPrincipal.UserId : User.UserId();

        /// <summary>
        /// Whether the current user is authenticated
        /// </summary>
        protected virtual bool IsAuthenticated => ApiClaimsPrincipal != null ? ApiClaimsPrincipal.IsAuthenticated : User.IsAuthenticated();

        /// <summary>
        /// Whether the current user is unauthenticated
        /// </summary>
        protected virtual bool IsUnauthenticated => ApiClaimsPrincipal != null ? ApiClaimsPrincipal.IsUnauthenticated : User.IsUnauthenticated();

        /// <summary>
        /// Requesting user's agent
        /// </summary>
        /// <value></value>
        protected string UserAgent
        {
            get
            {
                if (Request == null || Request.Headers == null || !Request.Headers.ContainsKey(HEADER_USER_AGENT))
                {
                    return string.Empty;
                }

                var userAgent = Request.Headers[HEADER_USER_AGENT];
                if (string.IsNullOrWhiteSpace(userAgent))
                {
                    return string.Empty;
                }

                return userAgent;
            }
        }

        /// <summary>
        /// Is the CurrentUser a SuperAdmin
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsSuperAdmin => ApiClaimsPrincipal != null ? ApiClaimsPrincipal.IsSuperAdmin : User.IsSuperAdmin();

        #endregion Protected Properties


        #region Public Methods

        public override ICulture CurrentCulture => ApiCulture ?? base.CurrentCulture;

        #endregion Public Methods


        #region Protected Methods

        protected List<Error> GetCustomErrorMessage(string errorMessage) => new List<Error>
        {
            new Error
            {
                ErrorType = ErrorType.Error,
                Key       = "",
                Message   = errorMessage,
            }
        };

        #endregion Protected Methods
    }
}