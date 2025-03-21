import 'dart:convert';

import 'package:kogebog_flutter/models/profile.dart';
import 'package:kogebog_flutter/models/recipe_ingredient.dart';
import 'package:uuid/uuid.dart';

class Recipe {
  final String id;
  final String name;
  final int amountOfServings;
  final String? image;
  final String? profileId;
  final Profile? profile;
  List<RecipeIngredient> recipeIngredients;

  Recipe({required this.id, required this.name, required this.amountOfServings, this.image, this.profileId, this.profile, required this.recipeIngredients});


  factory Recipe.fromJson(Map<String, dynamic> json) {
  try {
    print("Processing recipe JSON: ${json['name']}");
    
    var recipeIngredients = json['recipeIngredients'];
    print("Recipe ingredients type: ${recipeIngredients?.runtimeType}");
    
    List<RecipeIngredient> ingredients = [];
    if (recipeIngredients != null && recipeIngredients is List) {
      for (var ingredient in recipeIngredients) {
        try {
          ingredients.add(RecipeIngredient.fromJson(ingredient));
        } catch (e) {
          print("Error parsing ingredient: $e");
          print("Problematic ingredient: $ingredient");
        }
      }
    }
    
    return Recipe(
      id: json['id'] ?? '',
      name: json['name'] ?? '',
      amountOfServings: json['amountOfServings'] ?? 0,
      image: json['image'],
      profile: json['profile'] != null ? Profile.fromJson(json['profile']) : null,
      recipeIngredients: ingredients,
    );
  } catch (e) {
    print("Error in Recipe.fromJson: $e");
    // Return a minimal valid Recipe object
    return Recipe(
      id: json['id'] ?? 'error',
      name: json['name'] ?? 'Error Recipe',
      amountOfServings: 0,
      recipeIngredients: [],
    );
  }
}

  static String generateUniqueFileName() {
    final uuid = Uuid().v4();
    return 'recipe_image_$uuid.jpg';
  }
}
