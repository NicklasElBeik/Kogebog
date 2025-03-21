import 'package:kogebog_flutter/models/ingredient.dart';
import 'package:kogebog_flutter/models/recipe.dart';
import 'package:kogebog_flutter/models/unit.dart';

class RecipeIngredient {
  final String id;
  final double quantity;
  final String? unitId;
  final Unit? unit;
  final String? recipeId;
  final Recipe? recipe;
  final String? ingredientId;
  final Ingredient? ingredient;

  RecipeIngredient({required this.id, required this.quantity, this.unitId, this.unit, this.recipeId, this.recipe, this.ingredientId, this.ingredient});

  factory RecipeIngredient.fromJson(Map<String, dynamic> json) {
  try {
    return RecipeIngredient(
      id: json['id'] ?? '',
      quantity: json['quantity'] != null 
          ? (json['quantity'] is int 
              ? (json['quantity'] as int).toDouble() 
              : json['quantity'])
          : 0.0,
      unit: json['unit'] != null ? Unit.fromJson(json['unit']) : null,
      ingredient: json['ingredient'] != null ? Ingredient.fromJson(json['ingredient']) : null,
      // Don't try to parse recipe here to avoid circularity
      recipeId: json['recipeId'],
    );
  } catch (e) {
    print("Error in RecipeIngredient.fromJson: $e");
    return RecipeIngredient(
      id: json['id'] ?? 'error',
      quantity: 0.0,
    );
  }
}

  Map<String, dynamic> toJson() {
    return {
      "id": id,
      "quantity": "${quantity == quantity.toInt() ? quantity.toInt() : quantity.toStringAsFixed(2)}",
      "unitId": unitId,
      "unit": unit,
      "recipeId": recipeId,
      "recipe": recipe,
      "ingredientId": ingredientId,
      "ingredient": ingredient
    };
  }
}