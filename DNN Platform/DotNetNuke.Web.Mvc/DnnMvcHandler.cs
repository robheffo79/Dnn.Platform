﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information
namespace DotNetNuke.Web.Mvc
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.SessionState;

    using DotNetNuke.ComponentModel;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.HttpModules.Membership;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.UI.Modules;
    using DotNetNuke.Web.Mvc.Common;
    using DotNetNuke.Web.Mvc.Framework.Modules;
    using DotNetNuke.Web.Mvc.Routing;

    public class DnnMvcHandler : IHttpHandler, IRequiresSessionState
    {
        public static readonly string MvcVersionHeaderName = "X-AspNetMvc-Version";

        private ControllerBuilder controllerBuilder;

        public DnnMvcHandler(RequestContext requestContext)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }

            this.RequestContext = requestContext;
        }

        public static bool DisableMvcResponseHeader { get; set; }

        public RequestContext RequestContext { get; private set; }

        /// <inheritdoc/>
        bool IHttpHandler.IsReusable
        {
            get { return this.IsReusable; }
        }

        internal ControllerBuilder ControllerBuilder
        {
            get
            {
                if (this.controllerBuilder == null)
                {
                    this.controllerBuilder = ControllerBuilder.Current;
                }

                return this.controllerBuilder;
            }

            set
            {
                this.controllerBuilder = value;
            }
        }

        protected virtual bool IsReusable
        {
            get { return false; }
        }

        /// <inheritdoc/>
        void IHttpHandler.ProcessRequest(HttpContext httpContext)
        {
            this.SetThreadCulture();
            MembershipModule.AuthenticateRequest(this.RequestContext.HttpContext, allowUnknownExtensions: true);
            this.ProcessRequest(httpContext);
        }

        protected internal virtual void ProcessRequest(HttpContextBase httpContext)
        {
            try
            {
                var moduleExecutionEngine = this.GetModuleExecutionEngine();

                // Check if the controller supports IDnnController
                var moduleResult =
                    moduleExecutionEngine.ExecuteModule(this.GetModuleRequestContext(httpContext));
                httpContext.SetModuleRequestResult(moduleResult);
                this.RenderModule(moduleResult);
            }
            finally
            {
            }
        }

        protected virtual void ProcessRequest(HttpContext httpContext)
        {
            HttpContextBase httpContextBase = new HttpContextWrapper(httpContext);
            this.ProcessRequest(httpContextBase);
        }

        private void SetThreadCulture()
        {
            var portalSettings = PortalController.Instance.GetCurrentSettings();
            if (portalSettings is null)
            {
                return;
            }

            var pageLocale = Localization.GetPageLocale(portalSettings);
            if (pageLocale is null)
            {
                return;
            }

            Localization.SetThreadCultures(pageLocale, portalSettings);
        }

        private IModuleExecutionEngine GetModuleExecutionEngine()
        {
            var moduleExecutionEngine = ComponentFactory.GetComponent<IModuleExecutionEngine>();

            if (moduleExecutionEngine == null)
            {
                moduleExecutionEngine = new ModuleExecutionEngine();
                ComponentFactory.RegisterComponentInstance<IModuleExecutionEngine>(moduleExecutionEngine);
            }

            return moduleExecutionEngine;
        }

        private ModuleRequestContext GetModuleRequestContext(HttpContextBase httpContext)
        {
            var moduleInfo = httpContext.Request.FindModuleInfo();
            var moduleContext = new ModuleInstanceContext() { Configuration = moduleInfo };
            var desktopModule = DesktopModuleControllerAdapter.Instance.GetDesktopModule(moduleInfo.DesktopModuleID, moduleInfo.PortalID);
            var moduleRequestContext = new ModuleRequestContext
            {
                HttpContext = httpContext,
                ModuleContext = moduleContext,
                ModuleApplication = new ModuleApplication(this.RequestContext, DisableMvcResponseHeader)
                {
                    ModuleName = desktopModule.ModuleName,
                    FolderPath = desktopModule.FolderName,
                },
            };

            return moduleRequestContext;
        }

        private void RenderModule(ModuleRequestResult moduleResult)
        {
            var writer = this.RequestContext.HttpContext.Response.Output;

            var moduleExecutionEngine = ComponentFactory.GetComponent<IModuleExecutionEngine>();

            moduleExecutionEngine.ExecuteModuleResult(moduleResult, writer);
        }
    }
}
