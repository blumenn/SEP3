package com.example.butchersdjassignment;

import java.time.LocalDate;
import java.util.ArrayList;

public abstract class AnimalRepositoryImpl implements AnimalRepository {
    AnimalRepository animalRepository;

    @Override
    public Animal createAnimal(Animal animal) {
        return null;
    }

    @Override
    public Animal getAnimalById(Long registrationNumber) {
        return animalRepository.getAnimalById(registrationNumber);
    }

    @Override
    public ArrayList<Animal> getAllAnimals() {
        return animalRepository.getAllAnimals();
    }

    @Override
    public ArrayList<Animal> getAnimalsByDate(LocalDate date) {
        ArrayList<Animal> allAnimals = new ArrayList<Animal>();
        ArrayList<Animal> animalsFromThisDate = new ArrayList<>();
        for (Animal animal : allAnimals
        ) {
            if (animal.getArrivalDate().toLocalDate().equals(date)) {
                animalsFromThisDate.add(animal);
            }
        }
        return animalsFromThisDate;
    }

    @Override
    public ArrayList<Animal> getAnimalsByFarm(String origin) {
        ArrayList<Animal> allAnimals = new ArrayList<Animal>();
        ArrayList<Animal> animalsFromThisOriginFarm = new ArrayList<>();
        for (Animal animal : allAnimals
        ) {
            if (animal.getOrigin().equalsIgnoreCase(origin)) {
                animalsFromThisOriginFarm.add(animal);
            }
        }
        return animalsFromThisOriginFarm;
    }
}
