using Microsoft.OpenApi;

namespace MyRoot.API.Swagger
{
    public class OpenApiOperationExtension : IOpenApiExtension
    {
        private readonly OpenApiOperation Operation;

        public OpenApiOperationExtension(OpenApiOperation operation)
        {
            Operation = operation;
        }

        public void Write(IOpenApiWriter writer, OpenApiSpecVersion version)
        {
            switch (version)
            {
                case OpenApiSpecVersion.OpenApi2_0:
                    Operation.SerializeAsV2(writer);
                    break;
                case OpenApiSpecVersion.OpenApi3_0:
                    Operation.SerializeAsV3(writer);
                    break;
                default:
                    Operation.SerializeAsV31(writer);
                    break;
            };
        }
    }
}
