package exercise_5.entities;

import jakarta.persistence.Basic;
import jakarta.persistence.Column;
import jakarta.persistence.DiscriminatorValue;
import jakarta.persistence.Entity;

@Entity
@DiscriminatorValue(value = "credit_card")
public class CreditCard extends BaseBillingDetail {
    @Basic
    @Column(name = "type", nullable = false)
    private CreditCardType type;

    @Basic
    @Column(name = "expiration_month", nullable = false)
    private Integer expirationMonth;

    @Basic
    @Column(name = "expiration_year", nullable = false)
    private Integer expirationYear;

    public CreditCardType getType() {
        return this.type;
    }

    public void setType(CreditCardType type) {
        this.type = type;
    }

    public Integer getExpirationMonth() {
        return this.expirationMonth;
    }

    public void setExpirationMonth(Integer expirationMonth) {
        this.expirationMonth = expirationMonth;
    }

    public Integer getExpirationYear() {
        return this.expirationYear;
    }

    public void setExpirationYear(Integer expirationYear) {
        this.expirationYear = expirationYear;
    }
}
