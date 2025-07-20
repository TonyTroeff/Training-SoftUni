package softuni.exam.areImported;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.junit.jupiter.MockitoExtension;
import softuni.exam.repository.AttractionRepository;
import softuni.exam.service.impl.AttractionServiceImpl;

@ExtendWith(MockitoExtension.class)
public class TestAttractionServiceAreImportedTrue {


   @InjectMocks
   private AttractionServiceImpl attractionService;
   @Mock
   private AttractionRepository mockAttractionRepository;

   @Test
   void areImportedShouldReturnFalse() {
       Mockito.when(mockAttractionRepository.count()).thenReturn(1L);
       Assertions.assertTrue(attractionService.areImported());
   }
}