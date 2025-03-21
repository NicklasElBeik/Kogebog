import 'dart:convert';  // For base64 decoding
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:kogebog_flutter/widgets/base64_widget.dart';
import 'package:kogebog_flutter/widgets/modern_button.dart';
import '../providers/recipe_provider.dart';

class RecipeListView extends ConsumerStatefulWidget {
  const RecipeListView({super.key});

  @override
  ConsumerState<RecipeListView> createState() => _RecipeListViewState();
}

class _RecipeListViewState extends ConsumerState<RecipeListView> {
  @override
  Widget build(BuildContext context) {
    final recipeList = ref.watch(recipeProvider);

    return Scaffold(
      appBar: AppBar(
        title: Text(
          "Mine opskrifter",
          style: GoogleFonts.staatliches(fontSize: 30),
        ),
      ),
      body: recipeList.isEmpty
        ? Center(
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: [
                Text(
                  "Du har endnu ikke oprettet nogen opskrifter.",
                  style: Theme.of(context).textTheme.bodyLarge,
                  textAlign: TextAlign.center,
                ),
                SizedBox(height: 20),
                Icon(
                  Icons.arrow_downward,
                  size: 40,
                  color: Theme.of(context).primaryColor,
                ),
                SizedBox(height: 20),
                ModernButton(
                  text: "Opret en ny opskrift",
                  onPressed: () => context.push('/recipe/new'),
                ),
              ],
            ),
          )
        : ListView.builder(
            itemCount: recipeList.length,
            itemBuilder: (context, index) {
              final recipe = recipeList[index];
              return Card(
                margin: const EdgeInsets.symmetric(vertical: 8, horizontal: 16),
                elevation: 4,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
                child: InkWell(
                  onTap: () => context.push('/recipe/${recipe.id}'),
                  borderRadius: BorderRadius.circular(12),
                  child: Padding(
                    padding: const EdgeInsets.all(16.0),
                    child: Row(
                      children: [
                        // Handle image or placeholder
                        ClipRRect(
                          borderRadius: BorderRadius.circular(8),
                          child: recipe.image == null
                              ? Image.asset(
                                  'lib/images/image-placeholder.png',
                                  width: 80,
                                  height: 80,
                                  fit: BoxFit.cover,
                                )
                              : buildImageFromBase64(recipe.image!, 80, 80),
                        ),
                        const SizedBox(width: 16),
                        // Recipe information (name and servings)
                        Expanded(
                          child: Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Text(
                                recipe.name,
                                style: Theme.of(context).textTheme.bodyLarge,
                                maxLines: 1,
                                overflow: TextOverflow.ellipsis,
                              ),
                              const SizedBox(height: 8),
                              Text(
                                'Antal serveringer: ${recipe.amountOfServings}',
                                style: Theme.of(context).textTheme.bodyMedium,
                              ),
                            ],
                          ),
                        ),
                      ],
                    ),
                  ),
                ),
              );
            },
          ),
      floatingActionButton: FloatingActionButton(
        backgroundColor: const Color(0xFF1e453e),
        foregroundColor: Colors.white,
        onPressed: () => context.push('/recipe/new'),
        child: Icon(Icons.add),
      ),
    );
  }
}
