package entities;

import orm.annotations.Column;
import orm.annotations.Entity;
import orm.annotations.Id;

import java.time.LocalDate;

@Entity(name = "users")
public class User {
    @Id
    @Column(name = "id")
    private Long id;

    @Column(name = "username")
    private String username;

    @Column(name = "display_name")
    private String displayName;

    @Column(name = "age")
    private Integer age;

    @Column(name = "registration")
    private LocalDate registration;

    public User() {
    }

    public User(String username, String displayName, Integer age, LocalDate registration) {
        this.username = username;
        this.displayName = displayName;
        this.age = age;
        this.registration = registration;
    }

    public Long getId() {
        return this.id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getUsername() {
        return this.username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getDisplayName() {
        return this.displayName;
    }

    public void setDisplayName(String displayName) {
        this.displayName = displayName;
    }

    public Integer getAge() {
        return this.age;
    }

    public void setAge(Integer age) {
        this.age = age;
    }

    public LocalDate getRegistration() {
        return this.registration;
    }

    public void setRegistration(LocalDate registration) {
        this.registration = registration;
    }
}
