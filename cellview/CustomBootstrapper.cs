using Nancy;
using Nancy.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cellview
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("content", @"Content"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("scripts", @"Scripts"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("leaflet", @"Scripts/leaflet"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("fonts", @"fonts"));
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("content/images", @"Content/images"));
        }
    }
}
