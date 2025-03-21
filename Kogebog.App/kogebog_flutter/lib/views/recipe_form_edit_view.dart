import 'package:collection/collection.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import 'package:image_picker/image_picker.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:kogebog_flutter/models/ingredient.dart';
import 'package:kogebog_flutter/models/recipe.dart';
import 'package:kogebog_flutter/models/recipe_ingredient.dart';
import 'package:kogebog_flutter/models/unit.dart';
import 'package:kogebog_flutter/providers/ingredient_provider.dart';
import 'dart:io';
import 'package:dropdown_search/dropdown_search.dart';
import 'package:kogebog_flutter/providers/profile_provider.dart';
import 'package:kogebog_flutter/providers/recipe_provider.dart';

import 'package:kogebog_flutter/providers/unit_provider.dart';
import 'package:kogebog_flutter/widgets/modern_button.dart';

class RecipeFormEditView extends ConsumerStatefulWidget {
  final String recipeId;

  const RecipeFormEditView({super.key, required this.recipeId});

  @override
  RecipeFormEditViewState createState() => RecipeFormEditViewState();
}

class RecipeFormEditViewState extends ConsumerState<RecipeFormEditView> {
  late TextEditingController _nameController;
  List<RecipeIngredient> recipeIngredients = [];
  File? _image;
  int servings = 2;
  Ingredient? currentIngredient;
  final _quantityController = TextEditingController();
  Unit? currentUnit;
  bool isValid = false;
  
  @override
  void initState() {
    super.initState();

    // Initialize the controllers with the current recipe values
    final recipe = ref.read(recipeProvider).firstWhereOrNull((r) => r.id == widget.recipeId);
    if (recipe != null) {
      _nameController = TextEditingController(text: recipe.name);
      servings = recipe.amountOfServings;
      recipeIngredients = recipe.recipeIngredients;
    }
  }

  bool get isFormValid {
    return _nameController.text.isNotEmpty &&
        servings > 0 &&
        recipeIngredients.isNotEmpty;
  }

  Future<void> _pickImage() async {
    final ImagePicker picker = ImagePicker();
    final XFile? image = await picker.pickImage(source: ImageSource.camera);
    if (image != null) {
      setState(() {
        _image = File(image.path);
      });
    }
  }

  void _addIngredient() {
    if (currentIngredient != null) {
      setState(() {
        recipeIngredients.add(RecipeIngredient(
          id: '',
          quantity: double.tryParse(_quantityController.text) ?? 0.0,
          unitId: currentUnit!.id,
          unit: currentUnit,
          ingredientId: currentIngredient!.id,
          ingredient: currentIngredient,
        ));
      });
    }

    currentIngredient = null;
    currentUnit = null;
    _quantityController.text = "";
    _checkValidInputsForIngredient();
  }

  Future<void> _saveChanges(String recipeId) async {
    final updatedName = _nameController.text;
    final updatedServings = servings;

    final recipe = ref.read(recipeProvider).firstWhereOrNull((r) => r.id == recipeId);

    if (recipe != null) {
      Recipe updatedRecipe = Recipe(
        id: recipeId,
        name: updatedName,
        amountOfServings: updatedServings,
        profileId: recipe.profile?.id,
        recipeIngredients: recipeIngredients.map((ri) => 
          RecipeIngredient(id: ri.id, quantity: ri.quantity, unitId: ri.unit?.id, ingredientId: ri.ingredient?.id )
        ).toList(),
      );

      ref.read(recipeProvider.notifier).updateRecipe(updatedRecipe);
      context.pop();
    }
  }

  void _checkValidInputsForIngredient() {
    setState(() {
      isValid = currentIngredient != null && currentUnit != null && (double.tryParse(_quantityController.text) ?? 0.0) > 0;
    });
  }

  void _updateFormState() {
    setState(() {}); // Rebuild the widget whenever an input is updated
  }

  @override
  Widget build(BuildContext context) {
    final units = ref.watch(unitProvider);
    final ingredients = ref.watch(ingredientProvider);
    if (units.isEmpty || ingredients.isEmpty) 
    {
      return Center(child: CircularProgressIndicator());
    }

    return Scaffold(
    backgroundColor: Color(0xFFF0F0F0), // Lighter background for a modern look
    body: SingleChildScrollView(  // Wrapping the entire body with SingleChildScrollView
      child: Padding(
        padding: EdgeInsets.symmetric(horizontal: 20, vertical: 40),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // Back button and Title
            Row(
              children: [
                IconButton(
                  icon: Icon(Icons.arrow_back, color: Colors.black),
                  onPressed: () => context.pop(),
                ),
                Text(
                  "Ny opskrift",
                  style: GoogleFonts.lato(fontSize: 28, fontWeight: FontWeight.bold),
                ),
              ],
            ),
            SizedBox(height: 20),
            
            // Recipe Name
            TextField(
              controller: _nameController,
              onChanged: (value) => _updateFormState(),
              decoration: InputDecoration(
                labelText: "Opskriftens navn",
                labelStyle: TextStyle(color: Colors.black54),
                focusedBorder: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(8),
                  borderSide: BorderSide(color: const Color(0xFF1e453e)),
                ),
                enabledBorder: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(8),
                  borderSide: BorderSide(color: const Color(0xFF1e453e)),
                ),
              ),
            ),
            SizedBox(height: 20),

            // Image Preview and Upload
            GestureDetector(
              onTap: _pickImage,
              child: Container(
                height: 150,
                width: double.infinity,
                decoration: BoxDecoration(
                  color: Colors.grey[200],
                  borderRadius: BorderRadius.circular(8),
                ),
                child: _image == null
                    ? Icon(Icons.camera_alt, size: 50, color: Colors.black38)
                    : Image.file(_image!, fit: BoxFit.cover),
              ),
            ),
            SizedBox(height: 20),

            // Amount of Servings
            DropdownButtonFormField<int>(
              value: servings,
              onChanged: (newServings) {
                if (newServings != null) {
                  setState(() {
                    servings = newServings;
                  });
                }
              },
              items: List.generate(10, (index) => index + 1)
                  .map((value) {
                return DropdownMenuItem<int>(
                  value: value,
                  child: Text('$value servering(er)'),
                );
              }).toList(),
              decoration: InputDecoration(
                labelText: 'Antal serveringer',
                labelStyle: TextStyle(color: Colors.black54),
                border: OutlineInputBorder(borderRadius: BorderRadius.circular(8)),
              ),
            ),
            SizedBox(height: 20),
            Divider(),
            SizedBox(height: 20),

            // Ingredient Input and Quantity/Unit Section
            Row(
              children: [
                Expanded(
                  flex: 2,
                  child: DropdownSearch<Ingredient>(
                    items: (filter, infiniteScrollProps) => ingredients,
                    selectedItem: currentIngredient,
                    itemAsString: (Ingredient ing) => ing.name,
                    compareFn: (a, b) => a.id == b.id,
                    onChanged: (newIngredient) {
                      if (newIngredient != null) {
                        setState(() {
                          currentIngredient = newIngredient;
                        });
                        _checkValidInputsForIngredient();
                      }
                    },
                    popupProps: PopupProps.menu(
                      showSearchBox: true,
                      searchFieldProps: TextFieldProps(
                        decoration: InputDecoration(
                          labelText: "Søg...",
                          border: OutlineInputBorder(),
                        ),
                      ),
                    ),
                    decoratorProps: DropDownDecoratorProps(
                      decoration: InputDecoration(
                        labelText: "Ingrediens",
                        border: OutlineInputBorder(borderRadius: BorderRadius.circular(8)),
                      ),
                    ),
                  ),
                ),
                SizedBox(width: 10),

                // Quantity Input
              ],
            ),
                Row(
                  children: [
                    Expanded(
                      flex: 1,
                      child: TextField(
                        controller: _quantityController,
                        keyboardType: TextInputType.number,
                        inputFormatters: [
                        FilteringTextInputFormatter.allow(RegExp(r'^\d+\.?\d{0,2}')),
                        ],
                        decoration: InputDecoration(
                          labelText: "Mængde",
                          border: OutlineInputBorder(),
                        ),
                        onChanged: (value) =>_checkValidInputsForIngredient()
                      ),
                    ),
                    SizedBox(width: 10),
                    
                    // Unit Dropdown (Searchable)
                    Expanded(
                      flex: 2,
                      child: DropdownSearch<Unit>(
                        items: (filter, infiniteScrollProps) => units,
                        selectedItem: currentUnit,
                        itemAsString: (Unit unit) => unit.name,
                        compareFn: (a, b) => a.id == b.id,
                        onChanged: (newUnit) {
                          setState(() {
                            currentUnit = newUnit;
                          });
                          _checkValidInputsForIngredient();
                        },
                        decoratorProps: DropDownDecoratorProps(
                          decoration: InputDecoration(
                            labelText: "Enhed",
                            border: OutlineInputBorder(borderRadius: BorderRadius.circular(8)),
                          ),
                        ),
                        popupProps: PopupProps.menu(
                          showSearchBox: true,
                          searchFieldProps: TextFieldProps(
                            decoration: InputDecoration(
                              labelText: "Søg...",
                              border: OutlineInputBorder(),
                            ),
                          ),
                        ),
                      ),
                    ),
                  ],
                ),
            SizedBox(height: 20),

            // Add Ingredient Button
            ModernButton(
              width: double.infinity,
              onPressed: isValid ? _addIngredient : null,
              text: "Tilføj ingrediens",
            ),
            SizedBox(height: 20),
            Divider(),
            SizedBox(height: 20),

            // Ingredient List (with Quantity and Unit)
            Text("Ingredienser", style: GoogleFonts.lato(fontSize: 20)),
            SizedBox(height: 10),
            Column(
              children: recipeIngredients.map((ingredient) {
                return ListTile(
                  contentPadding: EdgeInsets.symmetric(vertical: 8),
                  title: Row(
                    children: [
                      Expanded(
                        child: Text(
                          "${ingredient.ingredient?.name} - ${ingredient.quantity == ingredient.quantity.toInt() ? ingredient.quantity.toInt() : ingredient.quantity.toStringAsFixed(2)} ${ingredient.unit?.abbreviation}",
                        ),
                      ),
                      IconButton(
                        icon: Icon(Icons.delete),
                        onPressed: () {
                          setState(() {
                            recipeIngredients.remove(ingredient);
                          });
                        },
                      ),
                    ],
                  ),
                );
              }).toList(),
            ),
            SizedBox(height: 40),
          ],
        ),
      ),
    ),
    floatingActionButton: AnimatedOpacity(
        opacity: isFormValid ? 1.0 : 0.0, // Show if valid, hide if invalid
        duration: Duration(milliseconds: 200), // Animation duration
        child: FloatingActionButton.extended(
          onPressed: isFormValid ? () => _saveChanges(widget.recipeId) : null,
          label: Text(
            "Gem opskrift",
            style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
          ),
          backgroundColor: isFormValid ? const Color(0xFF1e453e) : Colors.grey,
          foregroundColor: Colors.white,
        ),
      ),
  );
  }
}
