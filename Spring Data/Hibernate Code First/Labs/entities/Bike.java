package entities;

import jakarta.persistence.Entity;
import jakarta.persistence.Table;

// For "table-per-class" or "joined" setup:
@Table(name = "bikes")

// For "single-table" setup:
// @DiscriminatorValue("bike")

@Entity
public class Bike extends Vehicle {
}
