package place.run.mep.service;

import place.run.mep.entity.User;
import place.run.mep.entity.UserAuth;
import place.run.mep.repository.UserAuthRepository;
import place.run.mep.repository.UserRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.Collection;
import java.util.Collections;

@Service
@RequiredArgsConstructor
public class UserDetailsServiceImpl implements UserDetailsService {

    private final UserRepository userRepository;
    private final UserAuthRepository userAuthRepository;

    @Override
    @Transactional(readOnly = true)
    public UserDetails loadUserByUsername(String userId) throws UsernameNotFoundException {
        User user = userRepository.findByUserId(userId)
                .orElseThrow(() -> new UsernameNotFoundException("User not found with userId: " + userId));

        UserAuth userAuth = userAuthRepository.findById(user.getUserNo())
                .orElseThrow(() -> new UsernameNotFoundException("User auth record not found for userId: " + userId));

        // For simplicity, using a default role. Adapt if you have a roles system.
        // Collection<? extends GrantedAuthority> authorities = Collections.singletonList(new SimpleGrantedAuthority("ROLE_USER"));
        // If you store roles in UserAuth or another entity, load them here.
        // For example, if UserAuth has a getRole() method returning a string:
        // Collection<? extends GrantedAuthority> authorities = 
        //    Collections.singletonList(new SimpleGrantedAuthority(userAuth.getRole()));
        // Defaulting to ROLE_USER if no specific role logic is present yet
        Collection<? extends GrantedAuthority> authorities = Collections.singletonList(new SimpleGrantedAuthority("ROLE_USER"));


        return new org.springframework.security.core.userdetails.User(
                user.getUserId(),
                userAuth.getPasswordHash(), // Changed from getPassword()
                authorities
        );
    }
}
