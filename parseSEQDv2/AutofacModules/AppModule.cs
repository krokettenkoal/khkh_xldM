using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace parseSEQDv2.AutofacModules {
    class AppModule : Module {
        protected override void Load(ContainerBuilder builder) {
            base.Load(builder);

            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .AssignableTo<Form>();

            builder.Register(
                resolver => new YamlDotNet.Serialization.DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build()
            )
                .Named<IDeserializer>("yamlDe")
                .SingleInstance();

            builder.Register(
                resolver => new YamlDotNet.Serialization.SerializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build()
            )
                .Named<ISerializer>("yamlSer")
                .SingleInstance();
        }
    }
}
