import 'dart:convert';

import 'package:dio/dio.dart';
import 'package:kogebog_flutter/models/ingredient.dart';
import 'package:kogebog_flutter/models/profile.dart';
import 'package:kogebog_flutter/models/recipe.dart';
import 'package:kogebog_flutter/models/recipe_ingredient.dart';
import 'package:kogebog_flutter/models/unit.dart';
import 'package:kogebog_flutter/services/dio_service.dart';

class RecipeService {
  
  String baseUrl = '/recipe';

  Future<Recipe?> getById(String recipeId) async {
  try {
    final response = await DioService.dioJson.get('$baseUrl/$recipeId');
    if (response.statusCode == 200) {
      return Recipe.fromJson(response.data);
    }
  } catch (e) {
    print("Error fetching recipe: $e");
  }
  return null;
}

Future<List<Recipe>> getByProfileId(String profileId) async {
  try {
    final response = await DioService.dioJson.get('$baseUrl/profile/$profileId');
    
    if (response.statusCode != 200 || response.data == null) {
      print("Failed to fetch recipes or empty response. Status: ${response.statusCode}");
      return [];
    }
    
    if (response.data is! List) {
      print("Response data is not a List: ${response.data.runtimeType}");
      return [];
    }
    
    // Parse the recipes safely
    return parseRecipes(response.data as List);
    
  } catch (e) {
    print("Error fetching recipes: $e");
    return [];
  }
}

  Future<Recipe> create(Recipe recipe) async {
    try {
      FormData formData = FormData.fromMap({
        'id': recipe.id,
        'name': recipe.name,
        'amountOfServings': recipe.amountOfServings,
        'profileId': recipe.profileId,
        'recipeIngredients':
            recipe.recipeIngredients.map((e) => e.toJson()).toList(),
        // Dynamically generating the image file name
        if (recipe.image != null)
          'image': await MultipartFile.fromFile(
            recipe.image!,
            filename: "recipe-image.png",
          ),
      });

      final response = await DioService.dioMultipart.post(baseUrl, data: formData);

      if (response.statusCode == 200) {
        return parseRecipe(response.data);
      } else {
        throw Exception('Failed to create recipe');
      }
    } catch (e) {
      print("Error creating recipe: $e");
    }

    throw Error();
  }

  Future<Recipe> updateById(String id, Recipe recipe) async {
    try {
      FormData formData = FormData.fromMap({
        'id': id,
        'name': recipe.name,
        'amountOfServings': recipe.amountOfServings,
        'profileId': recipe.profileId,
        'recipeIngredients':
            recipe.recipeIngredients.map((e) => e.toJson()).toList(),
        // Dynamically generating the image file name
        if (recipe.image != null)
          'image': await MultipartFile.fromFile(
            recipe.image!,
            filename: "recipe-image.png",
          ),
      });
      final response = await DioService.dioJson.put('$baseUrl/$id', data: formData);

      return parseRecipe(response.data);
    }
    catch (e) {
      print("Error updating recipe: $e");
    }

    throw Error();
  }

  Future<void> deleteById(String id) async {
    try {
      await DioService.dioJson.delete('$baseUrl/$id');
    }
    catch (e) {
      print("Error deleting recipe: $e");
    }
  }

  // Helper method to parse recipes
List<Recipe> parseRecipes(List<dynamic> jsonList) {
  List<Recipe> recipes = [];
  
  for (var json in jsonList) {
    try {
      recipes.add(parseRecipe(json));
    } catch (e) {
      print("Error parsing recipe: $e");
    }
  }
  
  return recipes;
}

// Helper method to parse a single recipe
Recipe parseRecipe(Map<String, dynamic> json) {
  // Parse profile
  Profile? profile;
  if (json['profile'] is Map<String, dynamic>) {
    var profileJson = json['profile'];
    profile = Profile(
      id: profileJson['id'] ?? '',
      name: profileJson['name'] ?? ''
    );
  }
  
  // Parse recipe ingredients
  List<RecipeIngredient> ingredients = parseRecipeIngredients(json['recipeIngredients']);
  
  // Create recipe
  return Recipe(
    id: json['id'] ?? '',
    name: json['name'] ?? '',
    amountOfServings: json['amountOfServings'] ?? 0,
    image: json['image'],
    profile: profile,
    recipeIngredients: ingredients
  );
}

// Helper method to parse recipe ingredients
List<RecipeIngredient> parseRecipeIngredients(dynamic ingredientsJson) {
  List<RecipeIngredient> ingredients = [];
  
  if (ingredientsJson is! List) return ingredients;
  
  for (var ingJson in ingredientsJson) {
    try {
      // Parse ingredient
      Ingredient? ingredient;
      if (ingJson['ingredient'] is Map<String, dynamic>) {
        ingredient = Ingredient(
          id: ingJson['ingredient']['id'] ?? '',
          name: ingJson['ingredient']['name'] ?? ''
        );
      }
      
      // Parse unit
      Unit? unit;
      if (ingJson['unit'] is Map<String, dynamic>) {
        unit = Unit(
          id: ingJson['unit']['id'] ?? '',
          name: ingJson['unit']['name'] ?? '',
          pluralName: ingJson['unit']['pluralName'] ?? '',
          abbreviation: ingJson['unit']['abbreviation'] ?? ''
        );
      }
      
      // Create and add recipe ingredient
      ingredients.add(RecipeIngredient(
        id: ingJson['id'] ?? '',
        quantity: _parseQuantity(ingJson['quantity']),
        ingredient: ingredient,
        unit: unit
      ));
    } catch (e) {
      print("Error parsing ingredient: $e");
    }
  }
  
  return ingredients;
}

// Helper method to safely parse quantity
double _parseQuantity(dynamic value) {
  if (value == null) return 0.0;
  if (value is int) return value.toDouble();
  if (value is double) return value;
  if (value is String) {
    try {
      return double.parse(value);
    } catch (_) {
      return 0.0;
    }
  }
  return 0.0;
}
}