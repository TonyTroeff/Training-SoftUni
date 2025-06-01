package exercise_1.services;

import exercise_1.dtos.UserInputDto;
import exercise_1.models.User;
import exercise_1.repositories.UserRepository;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;

@Service
public class UserServiceImpl implements UserService {
    private final UserRepository repository;
    private final ModelMapper modelMapper;

    public UserServiceImpl(UserRepository repository, ModelMapper modelMapper) {
        this.repository = repository;
        this.modelMapper = modelMapper;
    }

    @Override
    public User createUser(UserInputDto dto) {
        User user = this.modelMapper.map(dto, User.class);
        return this.repository.save(user);
    }
}
