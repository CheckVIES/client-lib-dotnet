@rem nswag dotnet tool is used https://www.nuget.org/packages/NSwag.ConsoleCore
nswag openapi2csclient /input:spec.yaml ^
    /namespace:Deimdal.CheckVIES.Client.ApiClient ^
    /output:../ApiClient/ApiClient.generated.cs ^
    /dateType:System.DateOnly ^
    /timeType:System.TimeOnly ^
    /useBaseUrl:false ^
    /jsonLibrary:SystemTextJson ^
    /generateExceptionClasses:true ^
    /className:CheckViesClient ^
    /GenerateDefaultValues:false ^
    /GenerateOptionalPropertiesAsNullable:true ^
    /GenerateNullableReferenceTypes:true ^
    /RequiredPropertiesMustBeDefined:true ^
    /GenerateClientInterfaces:true
