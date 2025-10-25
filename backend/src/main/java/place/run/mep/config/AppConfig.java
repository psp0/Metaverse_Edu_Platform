package place.run.mep.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.client.RestTemplate;

@Configuration
public class AppConfig {
    // AI 서버와 HTTP 통신을 위한 Bean
    @Bean
    public RestTemplate restTemplate() {
        return new RestTemplate();
    }
}
