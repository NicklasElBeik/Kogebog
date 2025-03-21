import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:kogebog_flutter/models/recipe_ingredient.dart';
import 'package:kogebog_flutter/services/recipeIngredient_service.dart';
import 'package:collection/collection.dart';

class RecipeIngredientNotifier extends StateNotifier<List<RecipeIngredient>> {
  final RecipeIngredientService _recipeIngredientService;

  RecipeIngredientNotifier(this._recipeIngredientService) : super([]);

  Future<void> createRecipeIngredient(RecipeIngredient recipeIngredient) async {
    final newRecipeIngredient = await _recipeIngredientService.create(recipeIngredient);
    state = [...state, newRecipeIngredient];
  }

  Future<void> updateRecipeIngredient(RecipeIngredient updatedRecipeIngredient) async {
    await _recipeIngredientService.updateById(updatedRecipeIngredient.id, updatedRecipeIngredient);
    state = state.map((r) => r.id == updatedRecipeIngredient.id ? updatedRecipeIngredient : r).toList();
  }

  Future<void> deleteRecipeIngredient(String recipeIngredientId) async {
    await _recipeIngredientService.deleteById(recipeIngredientId);
    state = state.where((r) => r.id != recipeIngredientId).toList();
  }

  RecipeIngredient? getById(String recipeIngredientId) {
    return state.firstWhereOrNull((r) => r.id == recipeIngredientId);
  }
}

final recipeIngredientProvider = StateNotifierProvider<RecipeIngredientNotifier, List<RecipeIngredient>>((ref) {
  return RecipeIngredientNotifier(RecipeIngredientService());
});
