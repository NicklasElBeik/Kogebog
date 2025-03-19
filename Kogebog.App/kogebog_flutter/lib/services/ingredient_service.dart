import 'package:kogebog_flutter/models/ingredient.dart';
import 'package:kogebog_flutter/services/dio_service.dart';

class IngredientService {

  String baseUrl = 'ingredient';

  Future<List<Ingredient>> getAll() async {
    try {
      final response = await DioService.dio.get(baseUrl);

      if (response.statusCode == 200) {
        return (response.data as List).map((json) => Ingredient.fromJson(json)).toList();
      }
    }
    catch (e) {
      print("Error fetching ingredients: $e");
    }

    return [];
  }
}