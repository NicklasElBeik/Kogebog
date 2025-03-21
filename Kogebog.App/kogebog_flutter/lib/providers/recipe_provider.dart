import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:kogebog_flutter/providers/profile_provider.dart';
import 'package:kogebog_flutter/services/recipeIngredient_service.dart';
import 'package:kogebog_flutter/services/recipe_service.dart';
import '../models/recipe.dart';
import 'package:collection/collection.dart';

class RecipeNotifier extends StateNotifier<List<Recipe>> {
  final RecipeService _recipeService;
  final RecipeIngredientService _recipeIngredientService;
  final String _profileId;

  RecipeNotifier(this._recipeService, this._recipeIngredientService, this._profileId) : super([]) {
    _fetchRecipesOnce();
  }

  Future<void> _fetchRecipesOnce() async {
    if (state.isEmpty) {
      final recipes = await _recipeService.getByProfileId(_profileId);
      state = recipes;
    }
  }

  // Add a new recipe (both locally and in the API)
  Future<void> createRecipe(Recipe recipe) async {
    final newRecipe = await _recipeService.create(recipe);
    state = [...state, newRecipe];
  }

  // Update an existing recipe
  Future<void> updateRecipe(Recipe updatedRecipe) async {
    updatedRecipe = await _recipeService.updateById(updatedRecipe.id, updatedRecipe);
    state = state.map((r) => r.id == updatedRecipe.id ? updatedRecipe : r).toList();
  }

  // Delete a recipe
  Future<void> deleteRecipe(String recipeId) async {
    await _recipeService.deleteById(recipeId);
    state = state.where((r) => r.id != recipeId).toList();
  }

  Future<void> deleteRecipeIngredient(String recipeIngredientId) async {
  await _recipeIngredientService.deleteById(recipeIngredientId);

  state = state.map((recipe) {
    recipe.recipeIngredients = recipe.recipeIngredients.where((x) => x.id != recipeIngredientId).toList();
    
    return recipe; // Return the updated recipe
  }).toList();
}

  // Get a recipe by ID
  Recipe? getById(String recipeId) {
    return state.firstWhereOrNull((r) => r.id == recipeId);
  }
}

// Provider that initializes the RecipeNotifier once per app session
final recipeProvider = StateNotifierProvider<RecipeNotifier, List<Recipe>>((ref) {
  final profileId = ref.watch(profileProvider);
  if (profileId == null) {
    throw Exception("Profile ID is required before fetching recipes.");
  }
  return RecipeNotifier(RecipeService(), RecipeIngredientService(), profileId);
});
