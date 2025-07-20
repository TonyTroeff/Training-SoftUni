package softuni.exam.areImported;

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
public class TestVisitorServiceAreImportedFalse {


   @InjectMocks
   private VisitorServiceImpl visitorService;
   @Mock
   private VisitorRepository mockVisitorRepository;

   @Test
   void areImportedShouldReturnFalse() {
       Mockito.when(mockVisitorRepository.count()).thenReturn(0L);
       Assertions.assertFalse(visitorService.areImported());
   }
}