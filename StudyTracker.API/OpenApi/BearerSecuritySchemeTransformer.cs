using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace StudyTracker.API.OpenApi;

// Lägger in ett Bearer-security scheme i det genererade OpenAPI-dokumentet.
// Scalar-UI:t plockar upp schemat och visar en Authorize-knapp där man kan klistra
// in sin JWT — så att skyddade endpoints kan testas utan curl eller Postman.
public class BearerSecuritySchemeTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
        document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT-token från POST /api/auth/login. Klistra in enbart token — Scalar lägger till 'Bearer ' själv."
        };

        // Global requirement så att Scalar skickar Authorization-headern på alla anrop
        // när användaren klickat Authorize. Endpoints utan [Authorize] påverkas inte.
        var requirement = new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document, externalResource: null!)] = new List<string>()
        };

        document.Security ??= new List<OpenApiSecurityRequirement>();
        document.Security.Add(requirement);

        return Task.CompletedTask;
    }
}
