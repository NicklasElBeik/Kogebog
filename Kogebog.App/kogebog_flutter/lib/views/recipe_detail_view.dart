import 'package:collection/collection.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import 'package:kogebog_flutter/providers/recipe_ingredient_provider.dart';
import 'package:kogebog_flutter/widgets/base64_widget.dart';
import '../providers/recipe_provider.dart';

class RecipeDetailView extends ConsumerStatefulWidget {
  final String recipeId;

  const RecipeDetailView({super.key, required this.recipeId});

  @override
  RecipeDetailViewState createState() => RecipeDetailViewState();
}


class RecipeDetailViewState extends ConsumerState<RecipeDetailView> {
    Future<void> _deleteRecipeIngredient(String recipeIngredientId) async {
    ref
        .read(recipeProvider.notifier)
        .deleteRecipeIngredient(recipeIngredientId);
  }

  @override
  Widget build(BuildContext context) {
    final recipe = ref.watch(recipeProvider).firstWhereOrNull((r) => r.id == widget.recipeId);

    if (recipe == null) {
      return Scaffold(
        appBar: AppBar(title: Text("Recipe Details")),
        body: Center(child: Text("Recipe not found")),
      );
    }

    Future<void> deleteRecipe(String recipeId) async {
      ref.read(recipeProvider.notifier).deleteRecipe(recipeId);
      context.pop();
    }

    return Scaffold(
      appBar: AppBar(title: Text(recipe.name)),
      body: Padding(
        padding: EdgeInsets.all(16.0),
        child: Column(
          children: [
            recipe.image == null
                              ? Image.asset(
                                  'lib/images/image-placeholder.png',
                                  width: 200,
                                  height: 200,
                                  fit: BoxFit.cover,
                                )
                              : buildImageFromBase64(recipe.image!, 200, 200),
            SizedBox(height: 20),
            Text(recipe.name, style: TextStyle(fontSize: 22, fontWeight: FontWeight.bold)),
            SizedBox(height: 10),
            Text("Ingredienser:", style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold)),
            Divider(height: 20),
            ...recipe.recipeIngredients.map((recipeIngredient) => 
              Column(
                children: [
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Text(recipeIngredient.ingredient!.name, style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold)),
                      Row(
                        children: [
                          Text('${recipeIngredient.quantity == recipeIngredient.quantity.toInt() ? recipeIngredient.quantity.toInt() : recipeIngredient.quantity.toStringAsFixed(2)} ${recipeIngredient.quantity > 1 ? recipeIngredient.unit!.pluralName : recipeIngredient.unit!.name}', style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold)),
                          IconButton(
                        icon: Icon(Icons.delete),
                        onPressed: () {
                          setState(() {
                            _deleteRecipeIngredient(recipeIngredient.id);
                          });
                        },
                      ),
                        ],
                      ),
                    ],
                  ),
                  Divider(height: 20),
                ],
              ),
            ),
          ],
        ),
      ),
      floatingActionButton: Stack(
        children: [
          // "Save Changes" button on the right with padding from the right edge
          Positioned(
            bottom: 0,
            right: 0,  // Adjusted for some space from the right edge
            child: FloatingActionButton.extended(
              heroTag: "edit",
              onPressed: () {
                // Save changes when the button is pressed
                context.push('/recipe/${recipe.id}/edit');
              },
              label: Text(
                "Ret opskrift",
                style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
              ),
              backgroundColor: Colors.blueAccent,
              foregroundColor: Colors.white,
            ),
          ),
          // "Delete Recipe" button on the left with padding from the left edge
          Positioned(
            bottom: 0,
            left: 30,  // Adjusted for some space from the left edge
            child: FloatingActionButton.extended(
              heroTag: "Slet opskrift",
              onPressed: () {
                // Delete the recipe when the button is pressed
                deleteRecipe(recipe.id);
              },
              label: Text(
                "Delete Recipe",
                style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
              ),
              backgroundColor: Colors.redAccent,
              foregroundColor: Colors.white,
            ),
          ),
        ],
      ),
    );
  }
}