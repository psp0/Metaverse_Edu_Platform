package place.run.mep.config;

import io.swagger.v3.oas.models.Components;
import io.swagger.v3.oas.models.OpenAPI;
import io.swagger.v3.oas.models.info.Info;
import io.swagger.v3.oas.models.security.SecurityRequirement;
import io.swagger.v3.oas.models.security.SecurityScheme;
import io.swagger.v3.oas.models.servers.Server;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class SwaggerConfig {

    @Value("${swagger.server.url:https://localhost}")
    private String serverUrl;

    @Value("${swagger.server.port:8080}")
    private String serverPort;

    @Bean
    public OpenAPI openAPI() {
        Info info = new Info()
                .title("Metaverse Education Platform API")
                .version("v1.0.0")
                .description("API for MEP");

        // Security Scheme for JWT
        SecurityScheme securityScheme = new SecurityScheme()
                .type(SecurityScheme.Type.HTTP).scheme("bearer").bearerFormat("JWT")
                .in(SecurityScheme.In.HEADER).name("Authorization");
        SecurityRequirement securityRequirement = new SecurityRequirement().addList("bearerAuth");

        Server server = new Server()
                .url(serverUrl + ":" + serverPort)
                .description("MEP Server");

        return new OpenAPI()
                .components(new Components().addSecuritySchemes("bearerAuth", securityScheme))
                .addSecurityItem(securityRequirement)
                .addServersItem(server)
                .info(info);
    }
}
