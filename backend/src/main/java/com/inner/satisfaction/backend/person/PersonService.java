package com.inner.satisfaction.backend.person;

import com.inner.satisfaction.backend.base.BadRequestException;
import com.inner.satisfaction.backend.base.BaseService;
import java.awt.image.BufferedImage;
import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.IOException;
import java.util.Base64;
import javax.imageio.ImageIO;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

@Service
public class PersonService extends BaseService<Person> {

  private final PersonRepository personRepository;
  private final String path;

  protected PersonService(
      PersonRepository personRepository,
      PersonValidation personValidation,
      @Value("${person.images.path}")
      String path) {
    super(personRepository, personValidation);
    this.personRepository = personRepository;
    this.path = path;
  }

  public Person findByCnic(String cnic) {
    return personRepository.findByCnic(cnic);
  }

  @Override
  public Person save(Person person) {
    try {
      String imagePath = person.getImagePath();
      byte[] imageByte = Base64.getDecoder().decode(imagePath.split(",")[1]);
      ByteArrayInputStream bis = new ByteArrayInputStream(imageByte);
      BufferedImage image = ImageIO.read(bis);
      bis.close();

      // write the image to a file
      File file = new File(path + File.separator + person.getCnic() + ".png");
      ImageIO.write(image, "png", file);
      String fileName = person.getCnic() + ".png";
      person.setImagePath(fileName);

      return super.save(person);
    } catch (IOException e) {
      throw new BadRequestException("Unable to Save Image, Invalid Image or Format", e);
    }
  }
}
