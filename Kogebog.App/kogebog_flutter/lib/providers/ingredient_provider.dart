import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:kogebog_flutter/models/ingredient.dart';
import 'package:kogebog_flutter/services/ingredient_service.dart';

class IngredientNotifier extends StateNotifier<List<Ingredient>> {
  final IngredientService _ingredientService;

  IngredientNotifier(this._ingredientService) : super([]) {
    fetchIngredients();
  }

  Future<void> fetchIngredients() async {
    final ingredients = await _ingredientService.getAll();
    state = ingredients;
  }
}

final ingredientProvider = StateNotifierProvider<IngredientNotifier, List<Ingredient>>((ref) {
  final ingredientService = IngredientService();
  return IngredientNotifier(ingredientService);
});
