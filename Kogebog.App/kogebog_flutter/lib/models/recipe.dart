import 'package:kogebog_flutter/models/recipe_ingredient.dart';

class Recipe {
  final String id;
  final String title;
  final String? image;
  final List<RecipeIngredient> recipeIngredients;

  Recipe({required this.id, required this.title, required this.image, required this.recipeIngredients});

  factory Recipe.fromJson(Map<String, dynamic> json) {
    return Recipe(
      id: json['id'],
      title: json['title'],
      image: json['imageUrl'],
      recipeIngredients: List<RecipeIngredient>.from(json['recipeIngredients']),
    );
  }
}
