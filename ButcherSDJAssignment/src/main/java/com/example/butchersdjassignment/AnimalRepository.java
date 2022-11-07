package com.example.butchersdjassignment;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.time.LocalDate;
import java.util.ArrayList;

@Repository
public interface AnimalRepository extends JpaRepository<Animal, Long> {
    public Animal createAnimal(Animal animal);
    public Animal getAnimalById(Long registrationNumber);
    public ArrayList<Animal> getAllAnimals();
    public ArrayList<Animal> getAnimalsByDate(LocalDate date);
    public ArrayList<Animal> getAnimalsByFarm(String origin);
}
