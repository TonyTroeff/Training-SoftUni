import entities.Project;
import jakarta.persistence.EntityManager;
import jakarta.persistence.EntityManagerFactory;
import jakarta.persistence.Persistence;
import jakarta.persistence.TypedQuery;

import java.time.format.DateTimeFormatter;
import java.util.Comparator;
import java.util.List;

public class Exercise8 {
    public static void main(String[] args) {
        List<Project> projects;

        try (EntityManagerFactory emf = Persistence.createEntityManagerFactory("application-unit")) {
            try (EntityManager em = emf.createEntityManager()) {
                // We can retrieve the top 10 projects in the desired order directly from the database, but this requires executing a more advanced query:
                // with top as (select p.project_id from projects as p order by p.start_date desc limit 10)
                // select * from projects as p where p.project_id in (select * from top) order by name
                TypedQuery<Project> query = em.createQuery("select p from Project as p order by p.startDate desc", Project.class)
                        .setMaxResults(10);
                projects = query.getResultStream().sorted(Comparator.comparing(Project::getName)).toList();
            }
        }

        for (Project project : projects) {
            DateTimeFormatter dateTimeFormatter = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss.S");
            System.out.printf("Project name: %s%n", project.getName());
            System.out.printf("    Project Description: %s%n", project.getDescription());
            System.out.printf("    Project Start Date:%s%n", project.getStartDate().format(dateTimeFormatter));
            System.out.printf("    Project End Date: %s%n", project.getEndDate());
        }
    }
}
