package com.example.butchersdjassignment;

import java.util.List;
import java.util.Optional;

public class AnimalService {
    Animal create(Animal animal);
    List<Animal> findAll();
    Optional<Animal> findById(int id);
    List<Animal> findByDate(int day, int month, int year);
    List<Animal> findByFarm(String farm);
}
