package lab_1.models;

import jakarta.persistence.*;

import java.util.HashSet;
import java.util.Set;


@Entity
@Table(name = "users")
public class User extends BaseEntity {
    @Basic(optional = false)
    @Column(name = "username", nullable = false, unique = true)
    private String username;

    @Basic
    @Column(name = "age")
    private Integer age;

    @OneToMany(mappedBy = "user")
    private Set<Account> accounts = new HashSet<>();

    public String getUsername() {
        return this.username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public Integer getAge() {
        return this.age;
    }

    public void setAge(Integer age) {
        this.age = age;
    }

    public Set<Account> getAccounts() {
        return this.accounts;
    }

    public void setAccounts(Set<Account> accounts) {
        this.accounts = accounts;
    }
}
