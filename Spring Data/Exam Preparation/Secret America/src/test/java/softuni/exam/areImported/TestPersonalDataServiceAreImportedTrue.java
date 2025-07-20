package softuni.exam.areImported;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.junit.jupiter.MockitoExtension;
import softuni.exam.repository.PersonalDataRepository;
import softuni.exam.service.impl.PersonalDataServiceImpl;

@ExtendWith(MockitoExtension.class)
public class TestPersonalDataServiceAreImportedTrue {

   @InjectMocks
   private PersonalDataServiceImpl service;
   @Mock
   private PersonalDataRepository mockPersonalDataRepository;

   @Test
   void areImportedShouldReturnFalse() {
       Mockito.when(mockPersonalDataRepository.count()).thenReturn(1L);
       Assertions.assertTrue(service.areImported());
   }
}