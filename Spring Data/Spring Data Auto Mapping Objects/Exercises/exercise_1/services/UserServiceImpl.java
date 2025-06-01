package exercise_1.services;

import exercise_1.dtos.UserDto;
import exercise_1.dtos.UserLoginDto;
import exercise_1.dtos.UserRegisterDto;
import exercise_1.models.User;
import exercise_1.repositories.UserRepository;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;

@Service
public class UserServiceImpl implements UserService {
    private final UserRepository userRepository;
    private final ModelMapper modelMapper;

    public UserServiceImpl(UserRepository userRepository, ModelMapper modelMapper) {
        this.userRepository = userRepository;
        this.modelMapper = modelMapper;
    }

    @Override
    public void registerUser(UserRegisterDto dto) {
        User user = this.modelMapper.map(dto, User.class);
        user.setIsAdmin(false);

        this.userRepository.save(user);
    }

    @Override
    public UserDto login(UserLoginDto dto) {
        User user = this.userRepository.findByEmailAndPassword(dto.getEmail(), dto.getPassword());
        if (user == null) return null;

        return this.modelMapper.map(user, UserDto.class);
    }

    @Override
    public User getById(Long id) {
        return this.userRepository.findById(id).orElse(null);
    }
}
