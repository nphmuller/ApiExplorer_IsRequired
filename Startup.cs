using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;

namespace ApiExplorer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt => opt.Conventions.Add(new ApiExplorerConvention()));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var apiDescriptionsProvider = app.ApplicationServices.GetRequiredService<IApiDescriptionGroupCollectionProvider>();
            var parameterDescription = apiDescriptionsProvider.ApiDescriptionGroups.Items.Single().Items.Single().ParameterDescriptions.Single();
            var isRequired = parameterDescription.ModelMetadata.IsRequired;
        }
    }

    public class ApiExplorerConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            application.ApiExplorer.IsVisible = true;
        }
    }

    [Route("controller")]
    public class MyController : ControllerBase
    {
        [HttpPost]
        public void MyAction(MyModel model)
        {
        }
    }

    public class MyModel
    {
        public int Param1 { get; set; }
    }
}
