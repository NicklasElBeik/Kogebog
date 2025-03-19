import 'package:kogebog_flutter/models/ingredient.dart';
import 'package:kogebog_flutter/models/recipe.dart';
import 'package:kogebog_flutter/models/unit.dart';

class RecipeIngredient {
  final String id;
  final double quantity;
  final Unit unit;
  final Recipe recipe;
  final Ingredient ingredient;

  RecipeIngredient({required this.id, required this.quantity, required this.unit, required this.recipe, required this.ingredient});

  factory RecipeIngredient.fromJson(Map<String, dynamic> json) {
    return RecipeIngredient(id: json['id'], quantity: json['quantity'], unit: Unit.fromJson(json['unit']), recipe: Recipe.fromJson(json['recipe']), ingredient: Ingredient.fromJson(json['ingredient']));
  }
}