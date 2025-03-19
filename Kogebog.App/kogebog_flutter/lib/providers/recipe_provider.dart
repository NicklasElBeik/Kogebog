import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:kogebog_flutter/services/recipe_service.dart';
import '../models/recipe.dart';

final recipeProvider = FutureProvider.family<List<Recipe>, String>((ref, profileId) async {
  return RecipeService().getByProfileId(profileId);
});

final recipeDetailProvider = FutureProvider.family<Recipe?, String>((ref, recipeId) async {
  return RecipeService().getById(recipeId);
});

