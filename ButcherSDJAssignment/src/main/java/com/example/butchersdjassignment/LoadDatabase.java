package com.example.butchersdjassignment;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.boot.CommandLineRunner;
import org.springframework.context.annotation.Bean;

import java.util.Date;

public class LoadDatabase {
    private static final Logger log = LoggerFactory.getLogger(LoadDatabase.class);

    @Bean
    CommandLineRunner initDatabase(AnimalRepository animalRepository) {

        return args -> {
            animalRepository.save(new Animal("Sheep",new Date() , 117L,"Ram Ranch"));
            animalRepository.save(new Animal("Cow", new Date(), 234L,"Woodie Wonders"));
            animalRepository.save(new Animal("Chicken", new Date(), 11L,"Kentucky Krunch"));

            animalRepository.findAll().forEach(animal -> log.info("Preloaded " + animal));
        };
    }
}
