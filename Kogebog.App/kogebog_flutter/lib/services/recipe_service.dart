import 'package:kogebog_flutter/models/recipe.dart';
import 'package:kogebog_flutter/services/dio_service.dart';

class RecipeService {
  
  String baseUrl = '/recipe';

  Future<Recipe?> getById(String recipeId) async {
  try {
    final response = await DioService.dio.get('$baseUrl/$recipeId');
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
      final response = await DioService.dio.get('$baseUrl/profile/$profileId');

      if (response.statusCode == 200) {
        return (response.data as List).map((json) => Recipe.fromJson(json)).toList();
      }
    }
    catch (e) {
      print("Error fetching ingredients: $e");
    }

    return [];
  }

  Future<Recipe> create(Recipe recipe) async {
    try {
      final response = await DioService.dio.post(baseUrl, data: recipe);

      return response.data;
    }
    catch (e) {
      print("Error creating recipe: $e");
    }

    throw Error();
  }

  Future<Recipe> updateById(String id, Recipe recipe) async {
    try {
      final response = await DioService.dio.put('$baseUrl/$id', data: recipe);

      return response.data;
    }
    catch (e) {
      print("Error creating recipe: $e");
    }

    throw Error();
  }

  Future<void> deleteById(String id) async {
    try {
      await DioService.dio.delete('$baseUrl/$id');
    }
    catch (e) {
      print("Error creating recipe: $e");
    }
  }
}