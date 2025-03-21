import 'package:kogebog_flutter/models/recipe_ingredient.dart';
import 'package:kogebog_flutter/services/dio_service.dart';

class RecipeIngredientService {
  
  String baseUrl = '/recipeingredient';

  Future<List<RecipeIngredient>> getByRecipeId(String recipeId) async {
    try {
      final response = await DioService.dioJson.get('$baseUrl/recipe/$recipeId');

      if (response.statusCode == 200) {
        return (response.data as List).map((json) => RecipeIngredient.fromJson(json)).toList();
      }
    }
    catch (e) {
      print("Error fetching recipeIngredient: $e");
    }

    return [];
  }

  Future<RecipeIngredient> create(RecipeIngredient recipeIngredient) async {
    try {
      final response = await DioService.dioJson.post(baseUrl, data: recipeIngredient);

      return response.data;
    }
    catch (e) {
      print("Error creating recipeIngredient: $e");
    }

    throw Error();
  }

  Future<RecipeIngredient> updateById(String id, RecipeIngredient recipeIngredient) async {
    try {
      final response = await DioService.dioJson.put('$baseUrl/$id', data: recipeIngredient);

      return response.data;
    }
    catch (e) {
      print("Error creating recipeIngredient: $e");
    }

    throw Error();
  }

  Future<void> deleteById(String id) async {
    try {
      await DioService.dioJson.delete('$baseUrl/$id');
    }
    catch (e) {
      print("Error creating recipeIngredient: $e");
    }
  }
}