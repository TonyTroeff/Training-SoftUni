package softuni.exam.areImported;
//TestVisitorServiceAreImportedFalse

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.junit.jupiter.MockitoExtension;
import softuni.exam.repository.VisitorRepository;
import softuni.exam.service.impl.VisitorServiceImpl;

@ExtendWith(MockitoExtension.class)
public class TestDeviceServiceAreImportedTrue {


   @InjectMocks
   private VisitorServiceImpl deviceService;
   @Mock
   private VisitorRepository mockVisitorRepository;

   @Test
   void areImportedShouldReturnFalse() {
       Mockito.when(mockVisitorRepository.count()).thenReturn(1L);
       Assertions.assertTrue(deviceService.areImported());
   }
}