import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/recipe_provider.dart';

class RecipeDetailView extends ConsumerWidget {
  final String recipeId;

  const RecipeDetailView({super.key, required this.recipeId});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final recipeAsync = ref.watch(recipeDetailProvider(recipeId));

    return Scaffold(
      appBar: AppBar(title: Text("Recipe Details")),
      body: recipeAsync.when(
        data: (recipe) {
          if (recipe == null) {
            return Center(child: Text("Recipe not found"));
          }

          return Padding(
            padding: EdgeInsets.all(16.0),
            child: Column(
              children: [
                Image.network(recipe.image ?? "", height: 200, fit: BoxFit.cover),
                SizedBox(height: 20),
                Text(recipe.title, style: TextStyle(fontSize: 22, fontWeight: FontWeight.bold)),
                SizedBox(height: 10),
                Text("Ingredients:", style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold)),
                ...recipe.recipeIngredients.map((recipeIngredient) => ListTile(title: Text(recipeIngredient.ingredient.name))),
              ],
            ),
          );
        },
        loading: () => Center(child: CircularProgressIndicator()),
        error: (error, stackTrace) => Center(child: Text("Error loading recipe")),
      ),
    );
  }
}
